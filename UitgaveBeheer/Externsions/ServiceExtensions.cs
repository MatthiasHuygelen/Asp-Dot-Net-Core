using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
