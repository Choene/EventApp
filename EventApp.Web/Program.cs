using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.Common;
using EventApp.Data.Services;
using System.Data.SQLite;
using EventApp.Data.Helpers;

namespace EventApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<EventDataService>();
            builder.Services.AddScoped<RegistrationService>();

            // DbConnection Service
            builder.Services.AddScoped<DbConnection>((serviceProvider)=> 
            { 
                return new SQLiteConnection("Data Source=eventapp.db"); 
            });

            var app = builder.Build();

            // Initialize the database (this will create tables if they don't exist)
            DatabaseHelper.InitializeDatabase("Data Source=eventapp.db");

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}