using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MovieWeb.Areas.Identity.IdentityHostingStartup))]
namespace MovieWeb.Areas.Identity
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