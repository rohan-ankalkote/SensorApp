using Application;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(config =>
{
    config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SensorContext>(o => o.UseSqlite(builder.Configuration["ConnectionStrings:Sqlite"]));
builder.Services.AddScoped<ISeedRepository, SeedRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<ISensorService, SensorService>();
builder.Services.AddAutoMapper(config => config.AddProfile<MappingProfile>());
builder.Services.AddScoped<IDeviceReadingRepository, DeviceReadingRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seedRepository = scope.ServiceProvider.GetRequiredService<ISeedRepository>();

    await seedRepository.SeedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.MapSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
