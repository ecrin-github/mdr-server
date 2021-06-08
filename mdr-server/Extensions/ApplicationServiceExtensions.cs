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
            
            services.AddScoped<ISearchApiRepository, SearchApiRepository>();
            services.AddScoped<IApiRepository, ApiRepository>();
            services.AddScoped<IStudyRepository, StudyRepository>();
            services.AddScoped<IObjectRepository, ObjectRepository>();
            
            return services;
        }
    }
}