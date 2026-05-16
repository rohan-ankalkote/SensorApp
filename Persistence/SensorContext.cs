using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class SensorContext(DbContextOptions<SensorContext> options) : DbContext(options)
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceReading> DeviceReadings { get; set; }
        public DbSet<ThresholdAlert> ThresholdAlerts { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>(b =>
            {
                b.Property(e => e.Name).HasMaxLength(100);
                b.Property(e => e.Location).HasMaxLength(100);
                b.Property(e => e.Status).IsRequired();
                b.Property(e => e.Type).IsRequired();
                b.Property(e => e.Threshold).IsRequired();
                b.Property(e => e.Unit).IsRequired();
            });

            modelBuilder.Entity<DeviceReading>(b =>
            {
                b.HasOne(e => e.Device).WithMany(e => e.Readings).HasForeignKey(e => e.DeviceId).IsRequired();
            });

            modelBuilder.Entity<ThresholdAlert>(b =>
            {
                b.Property(e => e.Message).HasMaxLength(200);
                b.HasOne(e => e.DeviceReading).WithMany(e => e.ThresholdAlerts).HasForeignKey(e => e.DeviceReadingId).IsRequired();
            });

            modelBuilder.Entity<AuditLog>(b =>
            {
                b.Property(e => e.Message).HasMaxLength(200);
                b.HasOne(e => e.Device).WithMany(e => e.AuditLogs).HasForeignKey(e => e.DeviceId).IsRequired();
            });
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DeviceType>().HaveConversion<string>();
            configurationBuilder.Properties<DeviceStatus>().HaveConversion<string>();
            configurationBuilder.Properties<Unit>().HaveConversion<string>();
        }
    }
}
