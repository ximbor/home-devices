using System;
using System.Collections.Generic;

namespace HomeDevices.Core.Database.Models
{
    public class Consumer
    {

        public Guid ConsumerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public List<Device> Devices { get; set; }

    }
}
