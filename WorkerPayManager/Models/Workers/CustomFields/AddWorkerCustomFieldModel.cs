using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPayManager.Models.Workers
{
    public class AddWorkerCustomFieldRequiredModel : IAddWorkerCustomFieldModel
    {
        public int FieldId { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Custom Field Required.")]
        public string Value { get; set; }      
        public bool IsRequired { get; set; }
        public AddWorkerCustomFieldRequiredModel(int fieldId, string name, bool isRequired, string value = "")
        {
            FieldId = fieldId;
            Name = name;
            IsRequired = isRequired;
        }

        public AddWorkerCustomFieldRequiredModel()
        {

        }
    }

    public class AddWorkerCustomFieldModel : IAddWorkerCustomFieldModel
    {
        public int FieldId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsRequired { get; set; }
        public AddWorkerCustomFieldModel(int fieldId, string name, bool isRequired, string value = "")
        {
            FieldId = fieldId;
            Name = name;
            IsRequired = isRequired;
        }
    }

    public interface IAddWorkerCustomFieldModel
    {
        public int FieldId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsRequired { get; set; }
    }
}
