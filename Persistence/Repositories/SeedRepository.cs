using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Persistence.Repositories
{
    public class SeedRepository(SensorContext sensorContext) : ISeedRepository
    {
        public async Task SeedAsync()
        {
            if(sensorContext.Devices.Any() && sensorContext.DeviceReadings.Any())
            {
                return;
            }

            var devices = new List<Device>()
            {
                new()
                {
                    Name = "snsr-01",
                    Location = "Building A|Room 1",
                    Type = DeviceType.TEMPRETURE,
                    Status = DeviceStatus.ACTIVE,
                    Threshold = 70,
                    Unit = Unit.CELCIUS,
                    Interval = 30,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Readings = [..Enumerable.Range(0, 100).Select(i => 
                    {
                        var time = DateTime.Now.AddHours(-i * 5);
                        return new DeviceReading
                        {
                            ReadingTime = time,
                            PrimaryValue = 65 + (i % 15),
                            SecondaryValue = 55 + (i % 20),
                            TertiaryValue = 1013 + (i % 5),
                            CreatedAt = DateTime.Now,
                        };
                    })]
                },
                new()
                {
                    Name = "snsr-02",
                    Location = "Building A|Room 2",
                    Type = DeviceType.TEMPRETURE,
                    Status = DeviceStatus.ACTIVE,
                    Threshold = 80,
                    Unit = Unit.CELCIUS,
                    Interval = 30,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Readings = [..Enumerable.Range(0, 100).Select(i =>
                    {
                        var time = DateTime.Now.AddHours(-i * 5);
                        return new DeviceReading
                        {
                            ReadingTime = time,
                            PrimaryValue = 70 + (i % 10),
                            SecondaryValue = 60 + (i % 15),
                            TertiaryValue = 1010 + (i % 8),
                            CreatedAt = DateTime.Now,
                        };
                    })]
                },
                new()
                {
                    Name = "snsr-03",
                    Location = "Building B|Floor 1",
                    Type = DeviceType.PRESSURE,
                    Status = DeviceStatus.ACTIVE,
                    Threshold = 2,
                    Unit = Unit.ATM,
                    Interval = 60,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                }
            };

            await sensorContext.Devices.AddRangeAsync(devices);
            await sensorContext.SaveChangesAsync();
        }
    }
}
