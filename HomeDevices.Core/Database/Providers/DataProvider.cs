using HomeDevices.Core.Database.Models;
using System;
using System.Collections.Generic;
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
            return await _deviceDataEntity.Add(Device);
        }

        public async Task<Device> DeviceDelete(Device Device)
        {
            return await _deviceDataEntity.Remove(Device);
        }

        public async Task<Device> DeviceDelete(Guid ConsumerId)
        {
            return await _deviceDataEntity.Remove(ConsumerId);
        }

        public async Task<Device> DeviceGet(Guid Id)
        {
            return await _deviceDataEntity.Get(Id);
        }

        public async Task<IEnumerable<Device>> DevicesGet(Func<Device, bool> Predicate)
        {
            return await _deviceDataEntity.Query(Predicate);
        }

        public async Task<IEnumerable<Device>> DevicesGet()
        {
            return await _deviceDataEntity.GetAll();
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

            return await _deviceDataEntity.Update(obj);
        }

        #endregion

        #region Consumers
        public async Task<Consumer> ConsumerAdd(Consumer Consumer)
        {
            return await _consumerDataEntity.Add(Consumer);
        }

        public async Task<Consumer> ConsumerDelete(Consumer Consumer)
        {
            return await _consumerDataEntity.Remove(Consumer);
        }

        public async Task<Consumer> ConsumerDelete(Guid ConsumerId)
        {
            return await _consumerDataEntity.Remove(ConsumerId);
        }

        public async Task<IEnumerable<Consumer>> ConsumerGet(Func<Consumer, bool> Predicate)
        {
            return await _consumerDataEntity.Query(Predicate);
        }

        public async Task<Consumer> ConsumerGet(Guid Id)
        {
            return await _consumerDataEntity.Get(Id);
        }

        public async Task<IEnumerable<Consumer>> ConsumersGet()
        {
            return await _consumerDataEntity.GetAll();
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

            return await _consumerDataEntity.Update(obj);
        }
        #endregion

    }
}
