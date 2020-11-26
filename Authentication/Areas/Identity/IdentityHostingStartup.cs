using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Authentication.Areas.Identity.IdentityHostingStartup))]
namespace Authentication.Areas.Identity
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