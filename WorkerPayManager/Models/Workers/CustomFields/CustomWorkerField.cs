using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerPayManager.Models.Companies;

namespace WorkerPayManager.Models.Workers
{
    public class CustomWorkerField
    {
        public int Id { get; set; }
        public Company Company { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
    }
}
