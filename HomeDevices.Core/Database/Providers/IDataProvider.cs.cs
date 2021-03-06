﻿using HomeDevices.Core.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeDevices.Core.Database.Providers
{
    public interface IDataProvider
    {
        #region Devices
        public Task<IEnumerable<Device>> DevicesGet(Func<Device, bool> Predicate);
        public Task<IEnumerable<Device>> DevicesGet();
        public Task<Device> DeviceGet(Guid Id);
        public Task<Device> DeviceAdd(Device Device);
        public Task<Device> DeviceUpdate(Device Device);
        public Task<Device> DeviceDelete(Device Device);
        public Task<Device> DeviceDelete(Guid DeviceId);

        #endregion

        #region Consumers
        public Task<IEnumerable<Consumer>> ConsumerGet(Func<Consumer, bool> Predicate);
        public Task<IEnumerable<Consumer>> ConsumersGet();
        public Task<Consumer> ConsumerGet(Guid Id);
        public Task<Consumer> ConsumerAdd(Consumer Consumer);
        public Task<Consumer> ConsumerUpdate(Consumer Consumer);
        public Task<Consumer> ConsumerDelete(Consumer Consumer);
        public Task<Consumer> ConsumerDelete(Guid ConsumerId);
        #endregion

    }
}
