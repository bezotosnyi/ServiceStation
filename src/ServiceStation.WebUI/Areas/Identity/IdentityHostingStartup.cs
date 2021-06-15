using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ServiceStation.WebUI.Areas.Identity.IdentityHostingStartup))]
namespace ServiceStation.WebUI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}