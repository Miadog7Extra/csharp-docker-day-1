using exercise.wwwapi.Data;
using exercise.wwwapi.DataModels;
using exercise.wwwapi.Endpoints;
using exercise.wwwapi.Repository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepository<Patient>, Repository<Patient>>();
builder.Services.AddScoped<IRepository<Doctor>, Repository<Doctor>>();
builder.Services.AddScoped<IRepository<Appointment>, Repository<Appointment>>();

builder.Services.AddDbContext<DataContext>(options =>
{
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"));
options.LogTo(message => Debug.WriteLine(message));
});


builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.ConfigurePatientEndpoint();
app.Run();

public partial class Program { }

