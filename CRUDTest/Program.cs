using CRUDTest.Models;
using CRUDTest.Interfaces;
using CRUDTest.Repository;
using Microsoft.EntityFrameworkCore;

namespace CRUDTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IProviderRep, ProviderRepository>();
            builder.Services.AddTransient<IOrderRep, OrderRepository>();
            builder.Services.AddTransient<IOrderWebRep, OrderWebRepository>();
            builder.Services.AddTransient<IOrderItemWebRep, OrderItemWebRepository>();
            builder.Services.AddTransient<IAllOrdersRep, AllOrdersRepository>();

            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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
        }
    }
}