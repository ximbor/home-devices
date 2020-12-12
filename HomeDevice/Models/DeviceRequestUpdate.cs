using HomeDevices.Core.Database.Models;
using System;

namespace HomeDevices.Models
{
    public class DeviceRequestUpdate
    {
        public bool Available { get; set; }
        public string ConsumerId { get; set; }
        public string ProjectId { get; set; }
        public string Description { get; set; }
        public string DeviceId { get; set; }
        public string Regionid { get; set; }
        public DateTime RegisteredOn { get; set; }
        public string RegistryId { get; set; }
        public string TenantId { get; set; }

        public Device ToDevice()
        {
            return new Device()
            {
                Available = Available,
                ConsumerId = Guid.Parse(ConsumerId),
                ProjectId = ProjectId,
                Description = Description,
                DeviceId = Guid.Parse(DeviceId),
                Regionid = Regionid,
                RegisteredOn = DateTime.Now,
                RegistryId = RegistryId,
                TenantId = Guid.Parse(TenantId)
            };
        }

    }
}
