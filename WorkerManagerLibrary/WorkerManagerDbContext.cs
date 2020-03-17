using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerManagerLibrary
{
    public class WorkerManagerDbContext : DbContext
    {
        public WorkerManagerDbContext(DbContextOptions<WorkerManagerDbContext> options)
        : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Worker> Workers { get; set; }
    }
}
