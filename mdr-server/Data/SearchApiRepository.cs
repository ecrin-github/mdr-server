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
        
        public async Task<List<StudyDto>> GetStudySearchResults(SearchApiQueryDto searchApiQueryDto)
        {
            int? startFrom = null;
            if (searchApiQueryDto.Page != null && searchApiQueryDto.PageSize != null)
            {
                startFrom = ((searchApiQueryDto.Page + 1) * searchApiQueryDto.PageSize) - searchApiQueryDto.PageSize;
                if (startFrom == 1 && searchApiQueryDto.PageSize == 1)
                {
                    startFrom = 0;
                }
            }

            SearchRequest<Study> searchRequest = null;
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
            int? startFrom = null;
            if (searchApiQueryDto.Page != null && searchApiQueryDto.PageSize != null)
            {
                startFrom = ((searchApiQueryDto.Page + 1) * searchApiQueryDto.PageSize) - searchApiQueryDto.PageSize;
                if (startFrom == 1 && searchApiQueryDto.PageSize == 1)
                {
                    startFrom = 0;
                }
            }

            SearchRequest<Object> searchRequest = null;
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