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
        private readonly CompanyService _companyService;

        public WorkerService(AppDbContext context, GlobalVariable globalVariable, CompanyService companyService)
        {
            _companyService = companyService;
            _context = context;
            _globalVariable = globalVariable;
        }

        /// <summary>
        /// Get's List of custom worker fields
        /// </summary>
        /// <returns>Task<List<CustomWorkerField>></returns>
        public async Task<List<CustomWorkerField>> GetCustomWorkerFieldsAsync()
        {
            if (_globalVariable.IsCompanySelected)
            {
                return await _context.CustomWorkerFields.Where(x => x.Company.Id == _globalVariable.SelectedCompanyId).ToListAsync();
            }
            else return new List<CustomWorkerField>();
        }

        public async Task<CustomWorkerField> GetCustomWorkerFieldByIdAsync(int id)
        {
            return await _context.CustomWorkerFields.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<(bool, string)> AddCustomWorkerFieldAsync(string name, bool isRequired)
        {
            if (_globalVariable.IsCompanySelected)
            {
                if(await customWorkerFieldExistsForCompanyAsync(_globalVariable.SelectedCompanyId, name))
                {
                    return (false, "Field already exists.");
                }

                CustomWorkerField customWorkerField = new CustomWorkerField();
                customWorkerField.Name = name;
                customWorkerField.IsRequired = isRequired;
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
                    if (await customWorkerFieldExistsForCompanyAsync(_globalVariable.SelectedCompanyId, editCustomWorkerFieldModel.Name) && !editCustomWorkerFieldModel.Name.Equals(customWorkerField.Name))
                    {
                        return (false, "Field already exists.");
                    }

                    customWorkerField.Name = editCustomWorkerFieldModel.Name;
                    customWorkerField.IsRequired = editCustomWorkerFieldModel.IsRequired;
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
                    _context.CustomWorkerFieldValues.FromSqlRaw("DELETE FROM CustomWorkerFieldValues WHERE CustomWorkerFieldId = @id", customWorkerField.Id);
                    _context.CustomWorkerFields.Remove(customWorkerField);
                    await _context.SaveChangesAsync();
                    return (true, "Removed");
                }
                else return (false, "Id not found.");
            }
            else return (false, "Company not selected.");
        }

        public async Task<(bool, string)> AddWorkerAsync(AddWorkerModel addWorkerModel)
        {
            if (_globalVariable.IsCompanySelected)
            {

                Worker worker = new Worker()
                {
                    FirstName = addWorkerModel.FirstName,
                    LastName = addWorkerModel.LastName,
                    DateOfBirth = addWorkerModel.BirthDate,
                    CreationDate = DateTime.Now,
                    IdentificationNumber = addWorkerModel.IdentificationNumber,
                };

                worker.Company = await _companyService.GetSelectedCompanyAsync();

                _context.Workers.Add(worker);
                await _context.SaveChangesAsync();

                foreach (var item in addWorkerModel.CustomFieldValues)
                {
                    CustomWorkerField customWorkerField = await GetCustomWorkerFieldByIdAsync(item.FieldId);
                    CustomWorkerFieldValue customWorkerFieldValue = new CustomWorkerFieldValue(customWorkerField, worker, item.Value);

                    _context.CustomWorkerFieldValues.Add(customWorkerFieldValue);
                    await _context.SaveChangesAsync();
                }

                return (true, "Worker Added.");
            }
            else return (false, "Company not selected.");
        }
        
        public async Task<(bool, string)> EditWorkerAsync(EditWorkerModel editWorkerModel)
        {
            if (_globalVariable.IsCompanySelected)
            {
                Worker worker = await _context.Workers.SingleOrDefaultAsync(x => x.Id == editWorkerModel.Id);

                if (worker != null)
                {
                    worker.IdentificationNumber = editWorkerModel.IdentificationNumber;
                    worker.FirstName = editWorkerModel.FirstName;
                    worker.DateOfBirth = editWorkerModel.BirthDate;

                    _context.Workers.Update(worker);

                    for (int i = 0; i < editWorkerModel.CustomFieldValues.Count; i++)
                    {
                        CustomWorkerFieldValue customWorkerFieldValue = _context.CustomWorkerFieldValues.SingleOrDefault(x => x.CustomWorkerField.Id == editWorkerModel.CustomFieldValues[i].FieldId && x.Worker.Id == worker.Id);
                        
                        if(customWorkerFieldValue != null)
                        {
                            customWorkerFieldValue.Value = editWorkerModel.CustomFieldValues[i].Value;
                            _context.Update(customWorkerFieldValue);
                        }
                        else
                        {
                            CustomWorkerField customWorkerField = await GetCustomWorkerFieldByIdAsync(editWorkerModel.CustomFieldValues[i].FieldId);

                            customWorkerFieldValue = new CustomWorkerFieldValue(customWorkerField, worker, editWorkerModel.CustomFieldValues[i].Value);

                            _context.CustomWorkerFieldValues.Add(customWorkerFieldValue);
                            await _context.SaveChangesAsync();
                        }
                    }
                    await _context.SaveChangesAsync();

                    return (true, "Updated");
                }
                else return (false, "Id's don't match.");
            }
            else return (false, "Company not selected.");
        }

        public async Task<List<Worker>> GetWorkersAsync()
        {
            if (_globalVariable.IsCompanySelected)
            {
                return await _context.Workers.Where(x => x.Company.Id == _globalVariable.SelectedCompanyId).ToListAsync();
            }
            else return new List<Worker>();
        }

        public async Task<List<CustomWorkerFieldValue>> GetCustomFieldValuesByWorkerId(int workerId)
        {
            if (_globalVariable.IsCompanySelected)
            {
                return await _context.CustomWorkerFieldValues.Where(x => x.Worker.Id == workerId).ToListAsync();
            }
            else return new List<CustomWorkerFieldValue>();
        }




        #region Private Methods

        private async Task<bool> customWorkerFieldExistsForCompanyAsync(int companyId, string fieldName)
        {
            if (companyId != 0)
            {
                CustomWorkerField customWorkerField = await _context.CustomWorkerFields.SingleOrDefaultAsync(x => x.Company.Id == companyId && x.Name.Equals(fieldName));
                if (customWorkerField != null)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }


        #endregion



    }
}
