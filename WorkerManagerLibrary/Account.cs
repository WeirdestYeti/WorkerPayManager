using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerManagerLibrary
{
    public class Account
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }   
        public string Password { get; set; }
        public string Description { get; set; }

        public List<Worker> Workers { get; set; }



        public DatabaseConnection DbConnection { get; set; }


    }
}
