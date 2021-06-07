using System;
using mdr_server.Interfaces;
using Nest;

namespace mdr_server.Services
{
    public class ElasticSearchService : IElasticSearchService
    {
        public ElasticClient GetConnection()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"));
            return new ElasticClient(settings);
        }
    }
}