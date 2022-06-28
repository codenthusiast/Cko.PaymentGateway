using Cko.PaymentGateway.Core.Contracts;
using Cko.PaymentGateway.Core.Repository;
using Cko.PaymentGateway.Data;
using Cko.PaymentGateway.Data.Repository;
using Cko.PaymentGateway.Service.Services;
using Cko.PaymentGateway.Service.ThirdParty;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option => option.UseMySQL(Environment.GetEnvironmentVariable("MYSQL_CONN_STRING")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddHttpClient<IAcquiringBankClient, CkoBankClient>(client =>
{
    client.BaseAddress = new Uri("http://cko-banksimulator");

});
builder.Services.AddScoped<IPaymentService, PaymentService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

using var scope = app.Services.CreateScope();
    var dbcontext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbcontext.Database.EnsureCreated();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
