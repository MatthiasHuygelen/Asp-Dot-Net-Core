using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CoronaApp.Areas.Identity.IdentityHostingStartup))]
namespace CoronaApp.Areas.Identity
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