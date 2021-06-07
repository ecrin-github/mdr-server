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
        private readonly IStudyMapper _studyMapper;
        private readonly IObjectMapper _objectMapper;

        public SearchRepository(IElasticSearchService elasticSearchService, 
            IStudyMapper studyMapper, IObjectMapper objectMapper)
        {
            _elasticSearchService = elasticSearchService;
            _studyMapper = studyMapper;
            _objectMapper = objectMapper;
        }
        
        public async Task<List<StudyDto>> GetStudySearchResults(ElasticQueryDto elasticQueryDto)
        {
            int? startFrom = null;
            if (elasticQueryDto.Page != null && elasticQueryDto.PageSize != null)
            {
                startFrom = ((elasticQueryDto.Page + 1) * elasticQueryDto.PageSize) - elasticQueryDto.PageSize;
                if (startFrom == 1 && elasticQueryDto.PageSize == 1)
                {
                    startFrom = 0;
                }
            }

            SearchRequest<Study> searchRequest = null;
            if (startFrom != null)
            {
                searchRequest = new SearchRequest<Study>(Indices.Index(elasticQueryDto.Index))
                {
                    From = startFrom,
                    Size = elasticQueryDto.PageSize,
                    Query = new RawQuery(JsonSerializer.Serialize(elasticQueryDto.ElasticQuery))
                };
            }
            else
            {
                searchRequest = new SearchRequest<Study>(Indices.Index(elasticQueryDto.Index))
                {
                    Query = new RawQuery(JsonSerializer.Serialize(elasticQueryDto.ElasticQuery))
                };
            }

            {
                var results = await _elasticSearchService.GetConnection().SearchAsync<Study>(searchRequest);
                var studies = results.Documents.ToList();

                return await _studyMapper.MapStudies(studies);
            }
        }
        
        public async Task<List<StudyDto>> GetObjectSearchResults(ElasticQueryDto elasticQueryDto)
        {
            int? startFrom = null;
            if (elasticQueryDto.Page != null && elasticQueryDto.PageSize != null)
            {
                startFrom = ((elasticQueryDto.Page + 1) * elasticQueryDto.PageSize) - elasticQueryDto.PageSize;
                if (startFrom == 1 && elasticQueryDto.PageSize == 1)
                {
                    startFrom = 0;
                }
            }

            SearchRequest<Object> searchRequest = null;
            if (startFrom != null)
            {
                searchRequest = new SearchRequest<Object>(Indices.Index(elasticQueryDto.Index))
                {
                    From = startFrom,
                    Size = elasticQueryDto.PageSize,
                    Query = new RawQuery(JsonSerializer.Serialize(elasticQueryDto.ElasticQuery))
                };
            }
            else
            {
                searchRequest = new SearchRequest<Object>(Indices.Index(elasticQueryDto.Index))
                {
                    Query = new RawQuery(JsonSerializer.Serialize(elasticQueryDto.ElasticQuery))
                };
            }

            {
                var results = await _elasticSearchService.GetConnection().SearchAsync<Object>(searchRequest);
                var objects = results.Documents.ToList();

                return await _objectMapper.MapObjects(objects);
            }
        }
    }
}