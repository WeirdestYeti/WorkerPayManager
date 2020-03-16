using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerManagerLibrary
{
    public class DatabaseConnection
    {
        public bool LocalDb { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }


    }
}
