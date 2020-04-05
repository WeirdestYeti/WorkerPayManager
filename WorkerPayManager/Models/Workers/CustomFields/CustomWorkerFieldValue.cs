using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPayManager.Models.Workers
{
    public class CustomWorkerFieldValue
    {
        [Key]
        public int Id { get; set; }
        public CustomWorkerField CustomWorkerField { get; set; }
        public Worker Worker { get; set; }
        public string Value { get; set; }

        public CustomWorkerFieldValue(CustomWorkerField customWorkerField, Worker worker, string value = "")
        {
            CustomWorkerField = customWorkerField;
            Worker = worker;
            if (value == null) Value = "";
            else Value = value;
        }

        public CustomWorkerFieldValue()
        {

        }
    }
}
