using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeDevices.Core.Utils
{
    public static class Serializer
    {
        public static string Serialize(dynamic obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
        }

    }
}
