using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task_DAL.Data;
using Task_DAL.Repository;
using Task_Entities.InterFaces;

namespace Task_Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            builder.Services.AddControllersWithViews();

            var conn = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DbTaskContext>(options =>
               options.UseSqlServer(conn));
             builder.Services.AddTransient(typeof(IHomeRepository<> ), typeof(HomeRepository<>));
         //   builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         //.AddJwtBearer(options =>
         //{
         //    // Configure options here
         //});
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
            app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}