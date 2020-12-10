using HomeDevices.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDevices.Database.Services
{
    public class DevicesService : IDevicesService
    {
        private readonly DevicesContext _dbContext;
        public DevicesService(DevicesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Device> All()
        {
            return _dbContext.Devices
                .OrderBy(x => x.RegisteredOn)
                .ToList();
        }

        public Device Find(Guid Id)
        {
            return _dbContext.Devices
                .FirstOrDefault(x => Id.Equals(x.DeviceId));
        }
    }
}
