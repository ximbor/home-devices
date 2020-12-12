using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Modules;
using DotNet.Testcontainers.Containers.WaitStrategies;
using HomeDevices.Core.Database;
using HomeDevices.Core.Database.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HomeDevices.Tests.Fixtures
{
    public class PostgreSQLFixture: IDisposable
    {
        protected static TestcontainersContainer _container = null;
        protected DataProvider _dataProvider = null;

        public const string CONTAINER_IMAGE = "postgres";
        public const string CONTAINER_NAME = "postgres-test";
        public const string DB_USER = "homedev";
        public const string DB_PWD = "homedev";
        public const string DB_DEFAULT = "DEVICES";
        public const int CONTAINER_PORT = 5432;
        public readonly string CONNECTION_STRING =        
            $"Host={System.Environment.MachineName};Database={DB_DEFAULT};Username={DB_USER};Password={DB_PWD}";

        public PostgreSQLFixture()
        {
            if(_container == null)
            {
                _container = BuildConsulContainer();
                _container.StartAsync();

                // Let's wait for the container startup.
                Task.Delay(20 * 1000).Wait();
            }

            var dbContext = new DevicesContext(new DbContextOptionsBuilder().UseNpgsql(CONNECTION_STRING).Options);
            _dataProvider = new DataProvider(dbContext);
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
              .WithEnvironment("POSTGRES_USER", DB_USER)
              .WithEnvironment("POSTGRES_PASSWORD", DB_PWD)
              .WithEnvironment("POSTGRES_DB", DB_DEFAULT)
              .WithPortBinding(CONTAINER_PORT, CONTAINER_PORT)
              .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(CONTAINER_PORT));

            return testcontainersBuilder.Build();
        }

        public void Dispose()
        {
        }

}
}
