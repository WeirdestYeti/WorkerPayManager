using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerPayManager.Models.Accounts;
using WorkerPayManager.Models.Companies;
using WorkerPayManager.Models.Workers;

namespace WorkerPayManager.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<CustomWorkerField> CustomWorkerFields { get; set; }
        public DbSet<CustomWorkerFieldValue> CustomWorkerFieldValues { get; set; }

    }
}
