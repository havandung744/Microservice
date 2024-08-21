using OrderApi.Data.Database;
using OrderApi.Infrastructure.Automapper;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

bool.TryParse(builder.Configuration["BaseServiceSettings:UseInMemoryDatabase"], out var useInMemory);
bool.TryParse(builder.Configuration["UseAadAuthentication"], out var useAadAuthentication);


if (!useInMemory)
{

}
else
{
    builder.Services.AddDbContext<OrderContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDatabase"));
    });
}

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
