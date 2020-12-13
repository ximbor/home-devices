using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDevices.Models
{
    public class HomeDevicesConfiguration
    {
        public string ServiceId { get; set; }
        public string ServiceListeningInterface { get; set; }
        public string DatabaseServer { get; set; }
        public string DatabaseName { get; set; }
        public string DatabaseUsername { get; set; }
        public string DatabasePassword { get; set; }
        public string LogLevel { get; set; }


        public string GetConnectionString()
        {
            return $"Host={DatabaseServer};Database={DatabaseName};Username={DatabaseUsername};Password={DatabasePassword}";
        }
    }
}
