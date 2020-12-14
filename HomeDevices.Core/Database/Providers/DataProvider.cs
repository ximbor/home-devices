using HomeDevices.Core.Database.Exceptions;
using HomeDevices.Core.Database.Models;
using HomeDevices.Core.Utils;
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
            if(Device.ConsumerId==null && Device.Consumer==null)
            {
                throw new EntityNotFoundException("Consumer id not provided while adding a device", Device);
            }

            if (Device.ConsumerId != null)
            {
                var consumer = ConsumerGet(Device.ConsumerId);
                if(consumer == null)
                {
                    throw new EntityNotFoundException("Could not add the device, consumer id does not exist.", Device);
                }
                
            } else
            {
                var consumer = ConsumerGet(Device.Consumer.ConsumerId);
                if (consumer == null)
                {
                    throw new EntityNotFoundException($"Could not add the device, consumer id {Device.Consumer.ConsumerId} does not exist.", Device);
                }
            }

            var result = await _deviceDataEntity.Add(Device);
            Log.Debug($"Device added - { Serializer.Serialize(result)}");
            return result;
        }

        public async Task<Device> DeviceDelete(Device Device)
        {
            if (Device == null)
            {
                throw new ApplicationException($"Please specify the device to delete.");
            }

            var result =  await _deviceDataEntity.Remove(Device);
            if (result == null)
            {
                throw new EntityNotFoundException($"Device with ID={Device.DeviceId} not found", Device.DeviceId);
            }
            Log.Debug($"Device removed - {Serializer.Serialize(result.DeviceId)}");
            return result;
        }

        public async Task<Device> DeviceDelete(Guid DeviceId)
        {
            var result = await _deviceDataEntity.Remove(DeviceId);
            if (result == null)
            {
                throw new EntityNotFoundException($"Device with ID={DeviceId} not found", DeviceId);
            }
            Log.Debug($"Device removed - {Serializer.Serialize(result.DeviceId)}");
            return result;
        }

        public async Task<Device> DeviceGet(Guid Id)
        {
            var result = await _deviceDataEntity.Get(Id);

            if(result == null)
            {
                throw new EntityNotFoundException($"Device with ID={Id} not found", Id);
            }

            Log.Debug($"Device got - {Serializer.Serialize(Id)}");
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

            if (Device.ConsumerId == null && Device.Consumer == null)
            {
                throw new ApplicationException("Consumer id not provided while updating a device");
            }

            if (Device.ConsumerId != null)
            {
                var consumer = ConsumerGet(Device.ConsumerId);
                if (consumer == null)
                {
                    throw new EntityNotFoundException("Could not update the device, consumer id does not exist.", Device.ConsumerId);
                }

            }
            else
            {
                var consumer = ConsumerGet(Device.Consumer.ConsumerId);
                if (consumer == null)
                {
                    throw new EntityNotFoundException($"Could not update the device, consumer id {Device.Consumer.ConsumerId} does not exist.",
                        Device.Consumer.ConsumerId);
                }
            }

            var obj = await DeviceGet(Device.DeviceId);

            if(obj == null)
            {
                throw new EntityNotFoundException($"Could not update the device, device not found.", Device.DeviceId);
            }

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

            Log.Debug($"Device updating (Id={Device.DeviceId}) - {Serializer.Serialize(obj)}");
            var result = await _deviceDataEntity.Update(obj);            
            Log.Debug($"Device updated - {Serializer.Serialize(result)}");
            return result;
        }

        #endregion

        #region Consumers
        public async Task<Consumer> ConsumerAdd(Consumer Consumer)
        {
            var result = await _consumerDataEntity.Add(Consumer);
            Log.Debug($"Consumer added - {Serializer.Serialize(result)}");
            return result;
        }

        public async Task<Consumer> ConsumerDelete(Consumer Consumer)
        {
            if (Consumer == null)
            {
                throw new ApplicationException($"Please specify the consumer to delete.");
            }

            var result = await _consumerDataEntity.Remove(Consumer);

            if (result == null)
            {
                throw new EntityNotFoundException($"Consumer with ID={Consumer.ConsumerId} not found", Consumer.ConsumerId);
            }

            Log.Debug($"Consumer removed - {Serializer.Serialize(result.ConsumerId)}");
            return result;
        }

        public async Task<Consumer> ConsumerDelete(Guid ConsumerId)
        {
            var result = await _consumerDataEntity.Remove(ConsumerId);

            if (result == null)
            {
                throw new EntityNotFoundException($"Consumer with ID={ConsumerId} not found", ConsumerId);
            }

            Log.Debug($"Consumer removed - {Serializer.Serialize(result.ConsumerId)}");
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

            if (result == null)
            {
                throw new EntityNotFoundException($"Consumer with ID={Id} not found", Id);
            }

            Log.Debug($"Consumer got - {Serializer.Serialize(Id)}");
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
            if (Consumer == null)
            {
                throw new ApplicationException($"Please provide a consumer to update.");
            }

            var obj = await ConsumerGet(Consumer.ConsumerId);

            if (obj == null)
            {
                throw new EntityNotFoundException($"Could not update the consumer, consumer not found.", Consumer.ConsumerId);
            }

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

            Log.Debug($"Device updating (Id={Consumer.ConsumerId}) - {Serializer.Serialize(obj)}");
            var result = await _consumerDataEntity.Update(obj);
            Log.Debug($"Consumer updated - {Serializer.Serialize(result)}");
            return result;
        }
        #endregion

    }
}
