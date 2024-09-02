using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderApi.Data.Database;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain.Entities;
using OrderApi.Infrastructure.Automapper;
using OrderApi.Messaging.Receive.Options.v1;
using OrderApi.Messaging.Receive.Receiver.v1;
using OrderApi.Service.v1.Command;
using OrderApi.Service.v1.Query;
using OrderApi.Service.v1.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddHealthChecks();
builder.Services.AddOptions();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ICustomerNameUpdateService).Assembly, typeof(GetOrderByCustomerGuidQueryHandler).Assembly);

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IRequestHandler<GetOrderByCustomerGuidQuery, List<Order>>, GetOrderByCustomerGuidQueryHandler>();
builder.Services.AddTransient<ICustomerNameUpdateService, CustomerNameUpdateService>();

bool.TryParse(builder.Configuration["BaseServiceSettings:UseInMemoryDatabase"], out var useInMemory);
bool.TryParse(builder.Configuration["UseAadAuthentication"], out var useAadAuthentication);

var connection = builder.Configuration.GetConnectionString("OrderDatabase");
builder.Services.AddDbContext<OrderContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDatabase"));
    options.UseSqlServer(connection, b => b.MigrationsAssembly("OrderApi"));
});

builder.Services.AddScoped<OrderContext>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Order Api",
        Description = "A simple API to create or pay orders",
        Contact = new OpenApiContact
        {
            Name = "Dunghv",
            Email = "hdung72267@gmail.com",
        }
    });
});

//setting for rabbitmq
var serviceClientSettingsConfig = builder.Configuration.GetSection("RabbitMq");
builder.Services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);
builder.Services.AddHostedService<CustomerFullNameUpdateReceiver>();

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

//app.UseAuthorization();

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
