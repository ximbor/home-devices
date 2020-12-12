using HomeDevices.Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace HomeDevices.Core.Database
{
    public class DevicesContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<Consumer> Consumers { get; set; }

        public DevicesContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
            InitDatabase();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitDevices(modelBuilder);
            InitConsumers(modelBuilder);
        }

        protected void InitDatabase()
        {
            Database.EnsureCreated();
            SaveChanges();
        }

        /// <summary>
        /// Initializes Devices DbSet
        /// </summary>
        /// <param name="modelBuilder">Related model builder.</param>
        protected void InitDevices(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>().Property(p => p.DeviceId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Device>().Property(p => p.ConsumerId).IsRequired();
            modelBuilder.Entity<Device>().Property(p => p.TenantId).IsRequired();
            modelBuilder.Entity<Device>().Property(p => p.DeviceId).IsRequired();
            modelBuilder.Entity<Device>().Property(p => p.ProjectId).IsRequired();
            modelBuilder.Entity<Device>().Property(p => p.Regionid).IsRequired();
            modelBuilder.Entity<Device>().Property(p => p.RegistryId).IsRequired();

            modelBuilder.Entity<Device>()
                .HasOne(device => device.Consumer)
                .WithMany(consumer => consumer.Devices)
                .HasForeignKey(device => device.ConsumerId);
        }

        /// <summary>
        /// Initializes Devices DbSet
        /// </summary>
        /// <param name="modelBuilder">Related model builder.</param>
        protected void InitConsumers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consumer>().Property(c => c.ConsumerId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Consumer>().Property(c => c.ConsumerId).IsRequired();
            modelBuilder.Entity<Consumer>().Property(c => c.Email).IsRequired();

        }

    }

}
