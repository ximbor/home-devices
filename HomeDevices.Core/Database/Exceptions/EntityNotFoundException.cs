using HomeDevices.Core.Database.Models;
using HomeDevices.Core.Utils;
using Serilog;
using System;

namespace HomeDevices.Core.Database.Exceptions
{
    public class EntityNotFoundException: Exception
    {
        public EntityNotFoundException(string message, ModelEntity entity) {
            Log.Error($"{message} - {Serializer.Serialize(entity)}");
        }

        public EntityNotFoundException(string message, string entityId)
        {
            Log.Error($"{message} - ID={entityId}");
        }

        public EntityNotFoundException(string message, Guid entityId)
        {
            Log.Error($"{message} - ID={entityId}");
        }
    }
}
