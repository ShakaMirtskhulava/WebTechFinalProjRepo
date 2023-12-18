using BusinessLayer.Services;
using DatabaseLayer;
using DatabaseLayer.Configuration;
using DatabaseLayer.Data;
using DatabaseLayer.Repositories;
using PresentationLayer.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<DatabaseConfigurationSettings>(builder.Configuration.GetSection("DatabaseConfigurationSettings"));

builder.Services
    .AddDbContext<AppDbContext>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<IUnitOfWork,UnitOfWork>()
    .AddScoped<IProductService, ProductService>()
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=HomePage}/{id?}");

app.Run();
