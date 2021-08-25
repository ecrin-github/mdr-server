#nullable enable
namespace mdr_server.Contracts.v1.Requests.Query
{
    public class StudyCharacteristicsRequest : BaseQueryRequest
    {
        public string? TitleContains { get; set; }

        public string LogicalOperator { get; set; }

        public string? TopicsInclude { get; set; }
    }
}