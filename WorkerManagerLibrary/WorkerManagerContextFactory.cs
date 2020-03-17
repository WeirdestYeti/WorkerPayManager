using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerManagerLibrary
{
    public class WorkerManagerContextFactory : IDesignTimeDbContextFactory<WorkerManagerDbContext>
    {
        public WorkerManagerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WorkerManagerDbContext>();
            optionsBuilder.UseSqlite("Data Source=WorkerManager.db");

            return new WorkerManagerDbContext(optionsBuilder.Options);
        }
    }
}
