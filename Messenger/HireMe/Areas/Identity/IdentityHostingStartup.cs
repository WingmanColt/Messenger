using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(HireMe.Areas.Identity.IdentityHostingStartup))]
namespace HireMe.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}