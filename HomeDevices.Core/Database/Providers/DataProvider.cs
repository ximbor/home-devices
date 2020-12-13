using HomeDevices.Core.Database.Models;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDevices.Core.Database.Providers
{
    public class DataProvider: IDataProvider
    {
        private readonly DevicesContext _dbContext;
        private readonly DataEntity<Device> _deviceDataEntity;
        private readonly DataEntity<Consumer> _consumerDataEntity;
        public DataProvider(DevicesContext dbContext)
        {
            _dbContext = dbContext;
            _consumerDataEntity = new DataEntity<Consumer>(_dbContext);
            _deviceDataEntity = new DataEntity<Device>(_dbContext);
        }

        #region Devices
        public async Task<Device> DeviceAdd(Device Device)
        {
            var result = await _deviceDataEntity.Add(Device);
            Log.Debug($"Device added - {Serialize(result)}");
            return result;
        }

        public async Task<Device> DeviceDelete(Device Device)
        {
            var result =  await _deviceDataEntity.Remove(Device);
            Log.Debug($"Device removed - {Serialize(result.DeviceId)}");
            return result;
        }

        public async Task<Device> DeviceDelete(Guid DeviceId)
        {
            var result = await _deviceDataEntity.Remove(DeviceId);
            Log.Debug($"Device removed - {Serialize(result.DeviceId)}");
            return result;
        }

        public async Task<Device> DeviceGet(Guid Id)
        {
            var result = await _deviceDataEntity.Get(Id);
            Log.Debug($"Device got - {Serialize(Id)}");
            return result;
        }

        public async Task<IEnumerable<Device>> DevicesGet(Func<Device, bool> Predicate)
        {
            var result = await _deviceDataEntity.Query(Predicate);
            Log.Debug($"Devices got - N. {result.ToList().Count}");
            return result;
        }

        public async Task<IEnumerable<Device>> DevicesGet()
        {
            var result = await _deviceDataEntity.GetAll();
            Log.Debug($"Devices got - N. {result.ToList().Count}");
            return result;
        }

        public async Task<Device> DeviceUpdate(Device Device)
        {
            var obj = await DeviceGet(Device.DeviceId);

            if (Device.Available != null)
            {
                obj.Available = Device.Available;
            }

            if (Device.ConsumerId != null)
            {
                obj.ConsumerId = Device.ConsumerId;
            }

            if (Device.Description != null)
            {
                obj.Description = Device.Description;
            }

            if (Device.ProjectId != null)
            {
                obj.ProjectId = Device.ProjectId;
            }

            if (Device.Regionid != null)
            {
                obj.Regionid = Device.Regionid;
            }

            if (Device.RegisteredOn != null)
            {
                obj.RegisteredOn = Device.RegisteredOn;
            }

            if (Device.RegistryId != null)
            {
                obj.RegistryId = Device.RegistryId;
            }

            if (Device.TenantId != null)
            {
                obj.TenantId = Device.TenantId;
            }

            var result = await _deviceDataEntity.Update(obj);            
            Log.Debug($"Device updated - {Serialize(result)}");
            return result;
        }

        #endregion

        #region Consumers
        public async Task<Consumer> ConsumerAdd(Consumer Consumer)
        {
            var result = await _consumerDataEntity.Add(Consumer);
            Log.Debug($"Consumer added - {Serialize(result)}");
            return result;
        }

        public async Task<Consumer> ConsumerDelete(Consumer Consumer)
        {
            var result = await _consumerDataEntity.Remove(Consumer);
            Log.Debug($"Consumer removed - {Serialize(result.ConsumerId)}");
            return result;
        }

        public async Task<Consumer> ConsumerDelete(Guid ConsumerId)
        {
            var result = await _consumerDataEntity.Remove(ConsumerId);
            Log.Debug($"Consumer removed - {Serialize(result.ConsumerId)}");
            return result;
        }

        public async Task<IEnumerable<Consumer>> ConsumerGet(Func<Consumer, bool> Predicate)
        {
            var result = await _consumerDataEntity.Query(Predicate);
            Log.Debug($"Consumers got - N. {result.ToList().Count}");
            return result;
        }

        public async Task<Consumer> ConsumerGet(Guid Id)
        {
            var result = await _consumerDataEntity.Get(Id);
            Log.Debug($"Consumer got - {Serialize(Id)}");
            return result;

        }

        public async Task<IEnumerable<Consumer>> ConsumersGet()
        {
            var result = await _consumerDataEntity.GetAll();
            Log.Debug($"Consumers got - N. {result.ToList().Count}");
            return result;
        }

        public async Task<Consumer> ConsumerUpdate(Consumer Consumer)
        {
            var obj = await ConsumerGet(Consumer.ConsumerId);

            if(Consumer.Address != null)
            {
                obj.Address = Consumer.Address;
            }

            if (Consumer.Devices != null)
            {
                obj.Devices = Consumer.Devices;
            }

            if (Consumer.Email != null)
            {
                obj.Email = Consumer.Email;
            }

            if (Consumer.FirstName != null)
            {
                obj.FirstName = Consumer.FirstName;
            }

            if (Consumer.LastName != null)
            {
                obj.LastName = Consumer.LastName;
            }

            var result = await _consumerDataEntity.Update(obj);
            Log.Debug($"Consumer updated - {Serialize(result)}");
            return result;
        }
        #endregion


        private string Serialize(dynamic obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
        }
    }
}
