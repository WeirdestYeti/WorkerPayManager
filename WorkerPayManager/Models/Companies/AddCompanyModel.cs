﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPayManager.Models.Companies
{
    public class AddCompanyModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Company name is too long.")]
        public string Name { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Customer is too long.")]
        public string Customer { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "Password needs to 7-16 characters.")]
        public string Password { get; set; }
    }
}