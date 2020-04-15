using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPayManager.Models.Workers
{
    public class EditWorkerModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "First Name needs to 2-50 characters.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Last Name needs to 2-50 characters.")]
        public string LastName { get; set; }
        [Display(Name = "Identification Number")]
        public int IdentificationNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [ValidateComplexType]
        public List<IAddWorkerCustomFieldModel> CustomFieldValues { get; set; }
    }
}
