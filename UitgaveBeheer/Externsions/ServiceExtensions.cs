using Microsoft.Extensions.DependencyInjection;
using UitgaveBeheer.Services;

namespace UitgaveBeheer.Externsions
{
    public static class ServiceExtensions
    {
        public static void AddPhotoService(this IServiceCollection services)
        {
            services.AddTransient<IPhotoService, PhotoService>();
        }
    }
}
