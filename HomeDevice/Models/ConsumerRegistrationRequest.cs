using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDevices.Models
{
    public class ConsumerRegistrationRequest
    {

        public string TenantId { get; set; }

        public string ConsumerId { get; set; }

        public string RsaKey { get; set; }
    }
}
