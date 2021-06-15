using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using ServiceStation.DAL.Models;

namespace ServiceStation.WebUI
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                var host = CreateHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var userManager = services.GetRequiredService<UserManager<Employee>>();
                        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                        await IdentityInitializer.InitializeAsync(userManager, rolesManager);
                    }
                    catch (Exception ex)
                    {
                        var _logger = services.GetRequiredService<ILogger<Program>>();
                        _logger.LogError(ex, "An error occurred while seeding the database.");
                    }
                }
                host.Run();
            }
            catch (Exception exeption)
            {
                logger.Error(exeption, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               })
           .ConfigureLogging(logging =>
           {
               logging.ClearProviders();
               logging.SetMinimumLevel(LogLevel.Trace);
           })
           .UseNLog();
    }
}
