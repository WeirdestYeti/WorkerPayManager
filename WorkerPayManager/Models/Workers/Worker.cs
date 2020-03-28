using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorkerPayManager.Models.Companies;

namespace WorkerPayManager.Models.Workers
{
    public class Worker
    {
        [Key]
        public int Id { get; set; }
        public Company Company { get; set; }
        public int IdentificationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<CustomWorkerFieldValue> CustomFieldValues { get; set; }
    }
}
