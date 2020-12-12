using HomeDevices.Database.Models;
using System;
using System.Collections.Generic;

namespace HomeDevices.Models
{
    public class ConsumerRequestAdd
    {

        public string Address {get; set;}
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Device> Devices { get; set; }


        public Consumer ToConsumer()
        {
            return new Consumer()
            {
                Address = Address,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Devices = Devices
            };
        }

    }
}
