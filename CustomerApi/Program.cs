using CustomerApi.Data.Database;
using CustomerApi.Data.Repository.v1;
using CustomerApi.Infrastructure.Automapper;
using CustomerApi.Messaging.Send.Option.v1;
using CustomerApi.Messaging.Send.Send.v1;
using CustomerApi.Messaging.Send.Sender.v1;
using CustomerApi.Service.v1.Command;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderApi.Data.Repository.v1;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
//builder.Services.AddTransient<ICustomerUpdateSender, CustomerUpdateSender>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(UpdateCustomerCommand).Assembly);

var connection = builder.Configuration.GetConnectionString("CustomerDatabase");
builder.Services.AddDbContext<CustomerContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDatabase"));
    options.UseSqlServer(connection, b => b.MigrationsAssembly("CustomerApi"));
});

builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMq"));

// add rabbitmq

builder.Services.AddTransient<ICustomerUpdateSender, CustomerUpdateSender>();
//builder.Services.AddHostedService<CustomerUpdateSender>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Customer Api",
        Description = "A simple API to create or update customers",
        Contact = new OpenApiContact
        {
            Name = "Dunghv",
            Email = "hdung72267@gmail.com",
        }
    });
});


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

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

app.MapControllerRoute(
    name: "default",    
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); 
