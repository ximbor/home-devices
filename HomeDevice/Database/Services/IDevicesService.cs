using HomeDevices.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDevices.Database.Services
{
    public interface IDevicesService
    {
        public IEnumerable<Device> All();
        public Device Find(Guid Id);

    }
}
