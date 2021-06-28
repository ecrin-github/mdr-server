using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using mdr_server.Contracts.v1.Requests.Query;
using mdr_server.Contracts.v1.Responses;
using mdr_server.Entities.Study;
using mdr_server.Interfaces;
using Nest;
using Object = mdr_server.Entities.Object.Object;

namespace mdr_server.Data
{
    public class RawQueryRepository : IRawQueryRepository
    {
        private readonly IElasticSearchService _elasticSearchService;
        private readonly IDataMapper _dataMapper;

        public RawQueryRepository(IElasticSearchService elasticSearchService, 
            IDataMapper dataMapper)
        {
            _elasticSearchService = elasticSearchService;
            _dataMapper = dataMapper;
        }

        private static int? CalculateStartFrom(int? page, int? pageSize)
        {
            if (page == null && pageSize == null) return null;
            var startFrom = ((page + 1) * pageSize) - pageSize;
            if (startFrom == 1 && pageSize == 1)
            {
                startFrom = 0;
            }
            return startFrom;
        }
        
        public async Task<BaseResponse> GetStudySearchResults(RawQueryRequest rawQueryRequest)
        {
            
            var startFrom = CalculateStartFrom(page: rawQueryRequest.Page, pageSize: rawQueryRequest.Size);

            SearchRequest<Study> searchRequest;
            if (startFrom != null)
            {
                searchRequest = new SearchRequest<Study>(Indices.Index("study"))
                {
                    From = startFrom,
                    Size = rawQueryRequest.Size,
                    Query = new RawQuery(JsonSerializer.Serialize(rawQueryRequest.ElasticQuery))
                };
            }
            else
            {
                searchRequest = new SearchRequest<Study>(Indices.Index("study"))
                {
                    Query = new RawQuery(JsonSerializer.Serialize(rawQueryRequest.ElasticQuery))
                };
            }

            {
                var results = await _elasticSearchService.GetConnection().SearchAsync<Study>(searchRequest);
                var studies = await _dataMapper.MapStudies(results.Documents.ToList());
                return new BaseResponse()
                {
                    Total = results.Total,
                    Data = studies
                };
            }
        }
        
        public async Task<BaseResponse> GetObjectSearchResults(RawQueryRequest rawQueryRequest)
        {
            
            var startFrom = CalculateStartFrom(page: rawQueryRequest.Page, pageSize: rawQueryRequest.Size);
            
            SearchRequest<Object> searchRequest;
            if (startFrom != null)
            {
                searchRequest = new SearchRequest<Object>(Indices.Index("data-object"))
                {
                    From = startFrom,
                    Size = rawQueryRequest.Size,
                    Query = new RawQuery(JsonSerializer.Serialize(rawQueryRequest.ElasticQuery))
                };
            }
            else
            {
                searchRequest = new SearchRequest<Object>(Indices.Index("data-object"))
                {
                    Query = new RawQuery(JsonSerializer.Serialize(rawQueryRequest.ElasticQuery))
                };
            }

            {
                var results = await _elasticSearchService.GetConnection().SearchAsync<Object>(searchRequest);
                var studies = await _dataMapper.MapObjects(results.Documents.ToList());

                return new BaseResponse()
                {
                    Total = results.Total,
                    Data = studies
                };
            }
        }
    }
}