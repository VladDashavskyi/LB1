#region
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DB.Contexts;
#endregion

namespace Sharable.MCRProcessor.Service
{
    /// <summary>
    ///     Custom service register
    /// </summary>
    public static class Bootstrap
    {
        /// <summary>
        ///     Custom service register
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TenantContext>();
            return services;
        }
       
    }
}