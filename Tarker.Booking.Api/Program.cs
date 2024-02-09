using Microsoft.EntityFrameworkCore;
using Tarker.Booking.Application.Interfaces;
using Tarker.Booking.Persistence.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseService>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));

builder.Services.AddScoped<IDatabaseService, DatabaseService>();

var app = builder.Build();

app.Run();