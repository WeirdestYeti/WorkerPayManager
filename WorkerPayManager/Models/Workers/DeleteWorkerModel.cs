using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPayManager.Models.Workers
{
    public class DeleteWorkerModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "First Name needs to 2-50 characters.")]
        public string Password { get; set; }
    }
}
