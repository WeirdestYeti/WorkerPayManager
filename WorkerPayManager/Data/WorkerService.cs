using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerPayManager.Models.Workers;

namespace WorkerPayManager.Data
{
    public class WorkerService
    {
        private readonly AppDbContext _context;
        private readonly GlobalVariable _globalVariable;

        public WorkerService(AppDbContext context, GlobalVariable globalVariable)
        {
            _context = context;
            _globalVariable = globalVariable;
        }

        public async Task<List<CustomWorkerField>> GetCustomWorkerFieldsAsync()
        {
            if (_globalVariable.IsCompanySelected)
            {
                return await _context.CustomWorkerFields.Where(x => x.Company.Id == _globalVariable.SelectedCompanyId).ToListAsync();
            }
            else return new List<CustomWorkerField>();
        }

        public async Task<(bool, string)> AddCustomWorkerFieldAsync(string name)
        {
            if (_globalVariable.IsCompanySelected)
            {
                CustomWorkerField customWorkerField = new CustomWorkerField();
                customWorkerField.Name = name;
                customWorkerField.Company = await _context.Companies.SingleOrDefaultAsync(x => x.Id == _globalVariable.SelectedCompanyId);
                _context.CustomWorkerFields.Add(customWorkerField);
                await _context.SaveChangesAsync();

                return (true, "Added");
            }
            else return (false, "Company is not selected.");
        }

        public async Task<(bool, string)> EditCustomWorkerFieldAsync(EditCustomWorkerFieldModel editCustomWorkerFieldModel)
        {
            if (_globalVariable.IsCompanySelected)
            {
                CustomWorkerField customWorkerField = await _context.CustomWorkerFields.SingleOrDefaultAsync(x => x.Id == editCustomWorkerFieldModel.Id);
                if (customWorkerField != null)
                {
                    customWorkerField.Name = editCustomWorkerFieldModel.Name;
                    _context.CustomWorkerFields.Update(customWorkerField);
                    await _context.SaveChangesAsync();
                    return (true, "Updated.");
                }
                else return (false, "Id not found.");
            }
            else return (false, "Company not selected.");
        }

        public async Task<(bool, string)> DeleteCustomWorkerFieldAsync(int id)
        {
            if (_globalVariable.IsCompanySelected)
            {
                CustomWorkerField customWorkerField = await _context.CustomWorkerFields.SingleOrDefaultAsync(x => x.Id == id);
                if (customWorkerField != null)
                {
                    _context.CustomWorkerFieldValues.RemoveRange(_context.CustomWorkerFieldValues.Where(x => x.CustomWorkerField.Id == customWorkerField.Id));
                    _context.CustomWorkerFields.Remove(customWorkerField);
                    await _context.SaveChangesAsync();
                    return (true, "Removed");
                }
                else return (false, "Id not found.");
            }
            else return (false, "Company not selected.");
        }

    }
}
