using HomeDevices.Controllers;
using HomeDevices.Core.Database.Models;
using HomeDevices.Models;
using HomeDevices.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace HomeDevices.Tests
{
    public class DevicesControllerTests : IClassFixture<PostgreSQLFixture>
    {

        PostgreSQLFixture fixture;

        public DevicesControllerTests(PostgreSQLFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Should_Get_A_List_Of_Devices()
        {
            var controller = new DevicesController(fixture.GetDataProvider());
            var response = await controller.GetDevices() as OkObjectResult;
            Assert.NotNull(response.Value);
            Assert.Equal(response.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Add_A_Device()
        {
            var consumerController = new ConsumersController(fixture.GetDataProvider());
            var consumer = (await consumerController.AddConsumer(new ConsumerRequestAdd() { 
                Email = "test@mail.com"
            }) as OkObjectResult).Value as Consumer;

            var device = new DeviceRequestAdd() {
                Available = true,
                Description = "Description...",
                ProjectId = "Project test",
                Regionid = "europe-west1",
                RegisteredOn = System.DateTime.Now,
                RegistryId = "my-registry",
                TenantId = Guid.NewGuid().ToString(),
                ConsumerId = consumer.ConsumerId.ToString()
            };
            //-------------------------------------------

            var controller = new DevicesController(fixture.GetDataProvider());
            var added = await controller.AddDevice(device) as OkObjectResult;
            Assert.NotNull(added.Value);
            Assert.Equal(added.StatusCode, (int)HttpStatusCode.OK);

            var foundItem = await controller.GetDevice((added.Value as Device).DeviceId.ToString()) as OkObjectResult;
            Assert.Equal(foundItem.Value as Device, added.Value);
        }

        [Fact]
        public async Task Should_Add_And_Update_A_Device()
        {
            var consumerController = new ConsumersController(fixture.GetDataProvider());
            var consumer = (await consumerController.AddConsumer(new ConsumerRequestAdd()
            {
                Email = "test@mail.com"
            }) as OkObjectResult).Value as Consumer;

            var device = new DeviceRequestAdd()
            {
                Available = true,
                Description = "Description...",
                ProjectId = "Project test",
                Regionid = "europe-west1",
                RegisteredOn = System.DateTime.Now,
                RegistryId = "my-registry",
                TenantId = Guid.NewGuid().ToString(),
                ConsumerId = consumer.ConsumerId.ToString()
            };
            //-------------------------------------------

            var controller = new DevicesController(fixture.GetDataProvider());
            var added = await controller.AddDevice(device) as OkObjectResult;
            Assert.NotNull(added.Value);
            Assert.Equal(added.StatusCode, (int)HttpStatusCode.OK);

            var foundItem = (await controller.GetDevice((added.Value as Device).DeviceId.ToString()) as OkObjectResult).Value as Device;
            Assert.Equal(foundItem, added.Value);

            var deviceUpdate = new DeviceRequestUpdate()
            {
                DeviceId = foundItem.DeviceId.ToString(),
                Available = false,
                Description = "Updated description...",
                ProjectId = "Updated project test",
                Regionid = "europe-west2",
                RegisteredOn = DateTime.Now,
                RegistryId = "my-registry updated",
                TenantId = Guid.NewGuid().ToString(),
                ConsumerId = consumer.ConsumerId.ToString()
            };

            var updated = await controller.UpdateDevice(deviceUpdate.DeviceId, deviceUpdate) as OkObjectResult;
            var updatedItem = (await controller.GetDevice((updated.Value as Device).DeviceId.ToString()) as OkObjectResult).Value as Device;
            Assert.False(updatedItem.Available);
            Assert.Equal("Updated description...", updatedItem.Description);
            Assert.Equal("Updated project test", updatedItem.ProjectId);
            Assert.Equal("europe-west2", updatedItem.Regionid);
            Assert.Equal("my-registry updated", updatedItem.RegistryId);
        }

        [Fact]
        public async Task Should_Add_And_Remove_A_Device()
        {
            var consumerController = new ConsumersController(fixture.GetDataProvider());
            var consumer = (await consumerController.AddConsumer(new ConsumerRequestAdd()
            {
                Email = "test@mail.com"
            }) as OkObjectResult).Value as Consumer;

            var device = new DeviceRequestAdd()
            {
                Available = true,
                Description = "Description...",
                ProjectId = "Project test",
                Regionid = "europe-west1",
                RegisteredOn = System.DateTime.Now,
                RegistryId = "my-registry",
                TenantId = Guid.NewGuid().ToString(),
                ConsumerId = consumer.ConsumerId.ToString()
            };
            //-------------------------------------------

            var controller = new DevicesController(fixture.GetDataProvider());
            var added = await controller.AddDevice(device) as OkObjectResult;
            Assert.NotNull(added.Value);
            Assert.Equal(added.StatusCode, (int)HttpStatusCode.OK);

            var foundItem = (await controller.GetDevice((added.Value as Device).DeviceId.ToString()) as OkObjectResult).Value as Device;
            Assert.Equal(foundItem, added.Value);

            var deleteResult = await controller.RemoveDevice(foundItem.DeviceId.ToString());
            var foundItem2 = await controller.GetDevice((added.Value as Device).DeviceId.ToString()) as NotFoundObjectResult;
            Assert.Equal(foundItem2.StatusCode, (int)HttpStatusCode.NotFound);

        }
    }

}

