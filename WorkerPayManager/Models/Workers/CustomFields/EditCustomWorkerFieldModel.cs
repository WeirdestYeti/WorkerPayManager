﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPayManager.Models.Workers
{
    public class EditCustomWorkerFieldModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name needs to 1-100 characters.")]
        public string Name { get; set; }
        public bool IsRequired { get; set; }
    }
}
