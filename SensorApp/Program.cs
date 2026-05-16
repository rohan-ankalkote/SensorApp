using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SensorContext>(o => o.UseSqlite(builder.Configuration["ConnectionStrings:Sqlite"]));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
