using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerPayManager.Models.Accounts;
using WorkerPayManager.Models.Companies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WorkerPayManager.Data
{
    public class CompanyService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly GlobalVariable _globalVariable;


        public CompanyService(AppDbContext context, UserManager<Account> userManager, SignInManager<Account> signInManager, AuthenticationStateProvider authenticationStateProvider, GlobalVariable globalVariable)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationStateProvider = authenticationStateProvider;
            _globalVariable = globalVariable;
        }
        public async Task<bool> CreateCompanyAsync(string name, string customer, string password)
        {
            var user = await GetAuthenticationClaimPrincipalAsync();

            if (_signInManager.IsSignedIn(user))
            {
                Company company = new Company();
                company.Name = name;
                company.Account = await _userManager.FindByNameAsync(user.Identity.Name);
                company.Customer = customer;
                company.Password = password;

                _context.Companies.Add(company);
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<(bool, string)> ChangePasswordAsync(ChangeCompanyPasswordModel changeCompanyPasswordModel)
        {
            var user = await GetAuthenticationClaimPrincipalAsync();
            if (_signInManager.IsSignedIn(user))
            {
                Company companyToUpdate = await _context.Companies.SingleOrDefaultAsync(x => x.Id == changeCompanyPasswordModel.Id);

                if (companyToUpdate != null)
                {
                    if (companyToUpdate.Password.Equals(changeCompanyPasswordModel.OldPassword))
                    {
                        companyToUpdate.Password = changeCompanyPasswordModel.Password;

                        _context.Companies.Update(companyToUpdate);
                        await _context.SaveChangesAsync();
                        return (true, "Updated");
                    }
                    else return (false, "Wrong Password.");
                }
                else return (false, "Id's don't match.");
            }
            else return (false, "Not Authorized");
        }

        public async Task<(bool, string)> EditCompanyAsync(EditCompanyModel company)
        {
            var user = await GetAuthenticationClaimPrincipalAsync();

            if (_signInManager.IsSignedIn(user))
            {
                Company companyToUpdate = await _context.Companies.SingleOrDefaultAsync(x => x.Id == company.Id);
                if (company.Id == companyToUpdate.Id)
                {
                    if (companyToUpdate.Password.Equals(company.ConfirPassword))
                    {
                        companyToUpdate.Customer = company.Customer;
                        companyToUpdate.Name = company.Name;

                        _context.Companies.Update(companyToUpdate);
                        await _context.SaveChangesAsync();
                        _globalVariable.SetCompany(companyToUpdate.Id, companyToUpdate.Name);
                        return (true, "Updated");
                    }
                    else return (false, "Wrong Password.");
                }
                else return (false, "Id's don't match.");
            }
            else return (false, "Not Authorized");
        }

        public async Task<List<Company>> GetCompaniesForAccountAsync()
        {
            var user = await GetAuthenticationClaimPrincipalAsync();
            if (_signInManager.IsSignedIn(user))
            {
                Account account = await _userManager.FindByNameAsync(user.Identity.Name);
                List<Company> companies = await _context.Companies.Where(x => x.Account.Id == account.Id).ToListAsync();

                return companies;
            }
            else return new List<Company>();
        }

        public async Task<List<Company>> GetCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<(bool, string)> SelectCompanyAsync(int id, string password)
        {
            var user = await GetAuthenticationClaimPrincipalAsync();
            if (_signInManager.IsSignedIn(user))
            {
                Company company = await _context.Companies.SingleOrDefaultAsync(x => x.Id == id);
                if (company != null)
                {
                    if (company.Account.UserName.Equals(user.Identity.Name))
                    {
                        if (company.Password.Equals(password))
                        {
                            _globalVariable.SetCompany(company.Id, company.Name);
                            return (true, "Login successful.");
                        }
                        else return (false, "Wrong Password.");
                    }
                    else return (false, "This account doesn't have accces to this company.");

                }
                else return (false, "Company not found.");
            }
            else return (false, "Not Authorized");
        }

        public async Task<Company> GetSelectedCompanyAsync()
        {
            var user = await GetAuthenticationClaimPrincipalAsync();
            if (_signInManager.IsSignedIn(user))
            {
                return await _context.Companies.SingleOrDefaultAsync(x => x.Id == _globalVariable.SelectedCompanyId);
            }
            else return null;
        }

        public async Task LogOutCompanyAsync()
        {
            var user = await GetAuthenticationClaimPrincipalAsync();
            if (_signInManager.IsSignedIn(user))
            {
                _globalVariable.SetCompany(0, null);
            }
        }

        private async Task<ClaimsPrincipal> GetAuthenticationClaimPrincipalAsync()
        {
            var authstate = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return authstate.User;      
        }


    }
}
