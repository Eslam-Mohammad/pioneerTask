using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using pioneerTask.Interfaces.Repository;
using pioneerTask.Interfaces.Services;
using pioneerTask.Mappings;
using pioneerTask.Models;
using pioneerTask.Repositories;
using pioneerTask.Services;

namespace pioneerTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("db")));

            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));
            // Register Repositories
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IPropertyDefinitionRepository, PropertyDefinitionRepository>();

            // Register Services
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IPropertyDefinitionService, PropertyDefinitionService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employees}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
