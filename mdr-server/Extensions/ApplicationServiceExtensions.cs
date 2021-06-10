using mdr_server.Data;
using mdr_server.Helpers;
using mdr_server.Interfaces;
using mdr_server.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace mdr_server.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IElasticSearchService, ElasticSearchService>();

            services.AddScoped<IDataMapper, DataMapper>();
            
            services.AddScoped<IQueryRepository, QueryRepository>();
            services.AddScoped<IRawQueryRepository, RawQueryRepository>();
            services.AddScoped<IFetchedDataRepository, FetchedDataRepository>();
            
            return services;
        }
    }
}