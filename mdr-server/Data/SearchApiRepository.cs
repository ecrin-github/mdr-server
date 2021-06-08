using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using mdr_server.DTOs.Queries;
using mdr_server.DTOs.Study;
using mdr_server.Entities.Study;
using mdr_server.Interfaces;
using Nest;
using Object = mdr_server.Entities.Object.Object;

namespace mdr_server.Data
{
    public class SearchApiRepository : ISearchApiRepository
    {
        private readonly IElasticSearchService _elasticSearchService;
        private readonly IDataMapper _dataMapper;

        public SearchApiRepository(IElasticSearchService elasticSearchService, 
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
        
        public async Task<List<StudyDto>> GetStudySearchResults(SearchApiQueryDto searchApiQueryDto)
        {
            
            var startFrom = CalculateStartFrom(page: searchApiQueryDto.Page, pageSize: searchApiQueryDto.PageSize);

            SearchRequest<Study> searchRequest;
            if (startFrom != null)
            {
                searchRequest = new SearchRequest<Study>(Indices.Index("study"))
                {
                    From = startFrom,
                    Size = searchApiQueryDto.PageSize,
                    Query = new RawQuery(JsonSerializer.Serialize(searchApiQueryDto.ElasticQuery))
                };
            }
            else
            {
                searchRequest = new SearchRequest<Study>(Indices.Index("study"))
                {
                    Query = new RawQuery(JsonSerializer.Serialize(searchApiQueryDto.ElasticQuery))
                };
            }

            {
                var results = await _elasticSearchService.GetConnection().SearchAsync<Study>(searchRequest);
                var studies = results.Documents.ToList();

                return await _dataMapper.MapStudies(studies);
            }
        }
        
        public async Task<List<StudyDto>> GetObjectSearchResults(SearchApiQueryDto searchApiQueryDto)
        {
            
            var startFrom = CalculateStartFrom(page: searchApiQueryDto.Page, pageSize: searchApiQueryDto.PageSize);
            
            SearchRequest<Object> searchRequest;
            if (startFrom != null)
            {
                searchRequest = new SearchRequest<Object>(Indices.Index("data-object"))
                {
                    From = startFrom,
                    Size = searchApiQueryDto.PageSize,
                    Query = new RawQuery(JsonSerializer.Serialize(searchApiQueryDto.ElasticQuery))
                };
            }
            else
            {
                searchRequest = new SearchRequest<Object>(Indices.Index("data-object"))
                {
                    Query = new RawQuery(JsonSerializer.Serialize(searchApiQueryDto.ElasticQuery))
                };
            }

            {
                var results = await _elasticSearchService.GetConnection().SearchAsync<Object>(searchRequest);
                var objects = results.Documents.ToList();

                return await _dataMapper.MapObjects(objects);
            }
        }
    }
}