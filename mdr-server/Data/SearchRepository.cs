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
    public class SearchRepository : ISearchRepository
    {
        private readonly IElasticSearchService _elasticSearchService;
        private readonly IDataMapper _dataMapper;

        public SearchRepository(IElasticSearchService elasticSearchService, 
            IDataMapper dataMapper)
        {
            _elasticSearchService = elasticSearchService;
            _dataMapper = dataMapper;
        }
        
        public async Task<List<StudyDto>> GetStudySearchResults(SearchQueryDto searchQueryDto)
        {
            int? startFrom = null;
            if (searchQueryDto.Page != null && searchQueryDto.PageSize != null)
            {
                startFrom = ((searchQueryDto.Page + 1) * searchQueryDto.PageSize) - searchQueryDto.PageSize;
                if (startFrom == 1 && searchQueryDto.PageSize == 1)
                {
                    startFrom = 0;
                }
            }

            SearchRequest<Study> searchRequest = null;
            if (startFrom != null)
            {
                searchRequest = new SearchRequest<Study>(Indices.Index(searchQueryDto.Index))
                {
                    From = startFrom,
                    Size = searchQueryDto.PageSize,
                    Query = new RawQuery(JsonSerializer.Serialize(searchQueryDto.ElasticQuery))
                };
            }
            else
            {
                searchRequest = new SearchRequest<Study>(Indices.Index(searchQueryDto.Index))
                {
                    Query = new RawQuery(JsonSerializer.Serialize(searchQueryDto.ElasticQuery))
                };
            }

            {
                var results = await _elasticSearchService.GetConnection().SearchAsync<Study>(searchRequest);
                var studies = results.Documents.ToList();

                return await _dataMapper.MapStudies(studies);
            }
        }
        
        public async Task<List<StudyDto>> GetObjectSearchResults(SearchQueryDto searchQueryDto)
        {
            int? startFrom = null;
            if (searchQueryDto.Page != null && searchQueryDto.PageSize != null)
            {
                startFrom = ((searchQueryDto.Page + 1) * searchQueryDto.PageSize) - searchQueryDto.PageSize;
                if (startFrom == 1 && searchQueryDto.PageSize == 1)
                {
                    startFrom = 0;
                }
            }

            SearchRequest<Object> searchRequest = null;
            if (startFrom != null)
            {
                searchRequest = new SearchRequest<Object>(Indices.Index(searchQueryDto.Index))
                {
                    From = startFrom,
                    Size = searchQueryDto.PageSize,
                    Query = new RawQuery(JsonSerializer.Serialize(searchQueryDto.ElasticQuery))
                };
            }
            else
            {
                searchRequest = new SearchRequest<Object>(Indices.Index(searchQueryDto.Index))
                {
                    Query = new RawQuery(JsonSerializer.Serialize(searchQueryDto.ElasticQuery))
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