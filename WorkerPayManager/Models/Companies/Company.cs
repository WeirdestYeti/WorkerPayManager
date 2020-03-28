using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WorkerPayManager.Models.Accounts;
using WorkerPayManager.Models.Workers;

namespace WorkerPayManager.Models.Companies
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public Account Account { get; set; }
        public string Name { get; set; }
        public string Customer { get; set; }    
        public string Password  { get; set; }
        public List<CustomWorkerField> CustomWorkerFields { get; set; }

    }
}
