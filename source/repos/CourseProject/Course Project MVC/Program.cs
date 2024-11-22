using AutoMapper;
using Business_Logic.Abstract;
using Business_Logic.Beton;
using Course_Project_MVC.Profiles;
using DAL.Abstract;
using DAL.Beton;
using DALef.DALs;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);



MapperConfiguration config = new MapperConfiguration
    (
        c =>
        {
            c.AddMaps(typeof(OrderDALEF).Assembly, typeof(OrderDetailsProfile).Assembly);
        }
    );

string connectionString = builder.Configuration.GetConnectionString("InventoryManager") ?? throw new Exception("didtn find the connection string");
builder.Services.AddSingleton<string>(connectionString)
                .AddSingleton<IMapper>(config.CreateMapper())
                .AddScoped<IManagerDAL, ManagerDAL>()
                .AddScoped<IWareDAL, WareDAL>()
                .AddScoped<MyIAuthenticationService, AuthenticationService>()
                .AddScoped<IWareInventoryDAL, InventoryDALEF>()
                .AddScoped<IOrderDAL, OrderDALEF>()
                .AddScoped<IInventoryManager, InventoryManager>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(42);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
    });
// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
