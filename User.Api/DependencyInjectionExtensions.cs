using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Business;

namespace User.Api
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddUserProviders(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IUserProvider, UserProvider>();

            return services;
        }
    }
}
