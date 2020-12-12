using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Modules;
using DotNet.Testcontainers.Containers.WaitStrategies;
using HomeDevices.Database;
using HomeDevices.Database.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HomeDevices.Tests.Fixtures
{
    public class PostgreSQLFixture: IDisposable
    {
        protected TestcontainersContainer _container = null;
        protected DataProvider _dataProvider = null;

        public const string CONTAINER_IMAGE = "postgres";
        public const string CONTAINER_NAME = "postgres-test";
        public const int CONTAINER_PORT = 5432;
        public readonly string CONNECTION_STRING = $"Host={System.Environment.MachineName};Database=DEVICES;Username=homedev;Password=homedev";

        public PostgreSQLFixture()
        {
            _container = BuildConsulContainer();
            _container.StartAsync();
            _dataProvider = new DataProvider(new DevicesContext(new DbContextOptionsBuilder().UseNpgsql(CONNECTION_STRING).Options));
        }

        public DataProvider GetDataProvider()
        {
            return _dataProvider;
        }

        protected TestcontainersContainer BuildConsulContainer()
        {
            var testcontainersBuilder = new TestcontainersBuilder<TestcontainersContainer>()
              .WithImage(CONTAINER_IMAGE)
              .WithName(CONTAINER_NAME)
              .WithPortBinding(CONTAINER_PORT, CONTAINER_PORT)
              .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432));

            return testcontainersBuilder.Build();
        }

        public async void DestroyContainer()
        {
            if (_container != null)
            {
                // await _container.CleanUpAsync();
                await _container.StopAsync();
                await _container.DisposeAsync();

                System.Diagnostics.Trace.WriteLine("Waiting for postreSQL to stop (5 sec.)...");
                Task.Delay(5000).Wait();
            }
        }

        public void Dispose()
        {
            DestroyContainer();
        }

}
}
