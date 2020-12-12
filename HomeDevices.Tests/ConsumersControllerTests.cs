using HomeDevices.Controllers;
using HomeDevices.Database.Models;
using HomeDevices.Models;
using HomeDevices.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace HomeDevices.Tests
{
    public class ConsumersControllerTests : IClassFixture<PostgreSQLFixture>
    {

        PostgreSQLFixture fixture;

        public ConsumersControllerTests(PostgreSQLFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Should_Get_A_List_Of_Consumers()
        {
            var controller = new ConsumersController(fixture.GetDataProvider());
            var response = await controller.GetConsumers() as OkObjectResult;
            Assert.NotNull(response.Value);
            Assert.Equal(response.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Add_A_consumer()
        {
            var consumer = new ConsumerRequestAdd() {
                Address = "Some address",
                Email = "mail@consumer.com",
                FirstName = "First name",
                LastName = "Last name",
                Devices = new List<Device>()
            };
            //-------------------------------------------

            var controller = new ConsumersController(fixture.GetDataProvider());
            var added = await controller.AddConsumer(consumer) as OkObjectResult;
            Assert.NotNull(added.Value);
            Assert.Equal(added.StatusCode, (int)HttpStatusCode.OK);

            var foundItem = await controller.GetConsumer((added.Value as Consumer).ConsumerId.ToString()) as OkObjectResult;
            Assert.Equal(foundItem.Value as Consumer, added.Value);
        }

        [Fact]
        public async Task Should_Add_And_Update_A_Consumer()
        {
            var consumer = new ConsumerRequestAdd()
            {
                Address = "Some address",
                Email = "mail@consumer.com",
                FirstName = "First name",
                LastName = "Last name",
                Devices = new List<Device>()
            };

            //-------------------------------------------

            var controller = new ConsumersController(fixture.GetDataProvider());
            var added = await controller.AddConsumer(consumer) as OkObjectResult;
            Assert.NotNull(added.Value);
            Assert.Equal(added.StatusCode, (int)HttpStatusCode.OK);
            var foundItem = (await controller.GetConsumer((added.Value as Consumer).ConsumerId.ToString()) as OkObjectResult).Value as Consumer;
            Assert.Equal(foundItem, added.Value);

            var consumerUpdate = new ConsumerRequestUpdate()
            {
                Id = foundItem.ConsumerId.ToString(),
                Address = "Some address updated",
                Email = "mail@consumer.com updated",
                FirstName = "First name updated",
                LastName = "Last name updated",
                Devices = new List<Device>()
            };

            var updated = await controller.UpdateConsumer(consumerUpdate.Id, consumerUpdate) as OkObjectResult;
            var updatedItem = (await controller.GetConsumer((updated.Value as Consumer).ConsumerId.ToString()) as OkObjectResult).Value as Consumer;
            Assert.Equal("Some address updated", updatedItem.Address);
            Assert.Equal("mail@consumer.com updated", updatedItem.Email);
            Assert.Equal("First name updated", updatedItem.FirstName);
            Assert.Equal("Last name updated", updatedItem.LastName);
            Assert.Empty(updatedItem.Devices);
        }

        [Fact]
        public async Task Should_Add_And_Remove_A_Consumer()
        {
            var consumer = new ConsumerRequestAdd()
            {
                Address = "Some address",
                Email = "mail@consumer.com",
                FirstName = "First name",
                LastName = "Last name",
                Devices = new List<Device>()
            };
            //-------------------------------------------

            var controller = new ConsumersController(fixture.GetDataProvider());
            var added = await controller.AddConsumer(consumer) as OkObjectResult;
            Assert.NotNull(added.Value);
            Assert.Equal(added.StatusCode, (int)HttpStatusCode.OK);

            var foundItem = (await controller.GetConsumer((added.Value as Consumer).ConsumerId.ToString()) as OkObjectResult)
                .Value as Consumer;
            Assert.Equal(foundItem, added.Value);
            var deleteResult = await controller.RemoveConsumer(foundItem.ConsumerId.ToString());
            foundItem = (await controller.GetConsumer((added.Value as Consumer).ConsumerId.ToString()) as OkObjectResult)
                .Value as Consumer;
            Assert.Null(foundItem);
        }


    }
}

