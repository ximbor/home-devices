using HomeDevices.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace HomeDevices.Database
{
    public class DevicesContext : DbContext
    {
        public DevicesContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .Property(p => p.DeviceId)
                .ValueGeneratedOnAdd();
        }

    }

}
