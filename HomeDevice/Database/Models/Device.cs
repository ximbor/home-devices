using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDevices.Database.Models
{
    public class Device
    {
        public string ProjectId { get; set; }

        public string RegistryId { get; set; }

        public System.Guid DeviceId { get; set; }

        public DateTime RegisteredOn { get; set; }

        public string Regionid { get; set; }

        public string Description { get; set; }

        public bool Available { get; set; }
    }
}
