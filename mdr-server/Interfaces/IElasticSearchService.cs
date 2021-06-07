using Nest;

namespace mdr_server.Interfaces
{
    public interface IElasticSearchService
    {
        ElasticClient GetConnection();
    }
}