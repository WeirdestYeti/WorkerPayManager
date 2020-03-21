using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerPayManager.Models.Accounts;
using WorkerPayManager.Models.Companies;
using Microsoft.EntityFrameworkCore;

namespace WorkerPayManager.Data
{
    public class CompanyService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public CompanyService(AppDbContext context, UserManager<Account> userManager, SignInManager<Account> signInManager, AuthenticationStateProvider authenticationStateProvider)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<bool> CreateCompanyAsync(string name, string customer, string password)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            
            if(_signInManager.IsSignedIn(user))
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

        public async Task<(bool, string)> EditCompanyAsync(EditCompanyModel company)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;


            if (_signInManager.IsSignedIn(user))
            {

                Company companyToUpdate = await _context.Companies.SingleOrDefaultAsync(x => x.Id == company.Id);
                if (company.Id == companyToUpdate.Id)
                {
                    if (companyToUpdate.Password.Equals(company.OldPassword))
                    {
                        companyToUpdate.Customer = company.Customer;
                        companyToUpdate.Name = company.Name;
                        companyToUpdate.Password = company.Password;

                        _context.Companies.Update(companyToUpdate);
                        await _context.SaveChangesAsync();
                        return (true, "Updated");
                    }
                    else return (false, "Old Password doesn't match.");
                }
                else return (false, "Id's don't match");

             
            }
            else return (false, "Not Authorized");
        }

        public async Task<List<Company>> GetCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }
    }
}
