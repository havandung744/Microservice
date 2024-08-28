using CustomerApi.Data.Database;
using CustomerApi.Infrastructure.Automapper;
using CustomerApi.Messaging.Send.Option.v1;
using CustomerApi.Messaging.Send.Send.v1;
using CustomerApi.Messaging.Send.Sender.v1;
using CustomerApi.Service.v1.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(UpdateCustomerCommand).Assembly);

var connection = builder.Configuration.GetConnectionString("CustomerDatabase");
builder.Services.AddDbContext<CustomerContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDatabase"));
    options.UseSqlServer(connection, b => b.MigrationsAssembly("CustomerApi.Data"));
});

builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMq"));

// add rabbitmq
builder.Services.AddTransient<ICustomerUpdateSender, CustomerUpdateSender>();
//builder.Services.AddHostedService<CustomerUpdateSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
