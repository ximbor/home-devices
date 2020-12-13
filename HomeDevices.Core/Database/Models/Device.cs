using System;
using System.Text.Json.Serialization;

namespace HomeDevices.Core.Database.Models
{
    public class Device
    {
        public Guid DeviceId { get; set; }

        public Guid TenantId { get; set; }

        public Guid ConsumerId { get; set; }

        public Consumer Consumer { get; set; }

        public string ProjectId { get; set; }

        public string RegistryId { get; set; }

        public DateTime RegisteredOn { get; set; }
        
        public string Regionid { get; set; }

        public string Description { get; set; }

        public bool? Available { get; set; }
    }
}
