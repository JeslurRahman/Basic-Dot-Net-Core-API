using BasicDotNetCoreAPI.Data;
using BasicDotNetCoreAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomerService, CustomerService>();

#region Configure the DbContext
//Configure the DbContext
/*
 * This is Code First Approach
 * To do the DB Migration You need to add the following Nugget Package
    1.Microsoft.EntityFrameworkCore
    2.Microsoft.EntityFrameworkCore.SqlServer
    3.Microsoft.EntityFrameworkCore.Tools

  *To do the DB Migration You need to execute following commands in the Package-Manager-Console
        1. Add-Migration "Initial Migration"
        2. Update-Database
 */
builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
