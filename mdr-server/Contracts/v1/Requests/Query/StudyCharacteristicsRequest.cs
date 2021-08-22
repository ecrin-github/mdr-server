#nullable enable
namespace mdr_server.Contracts.v1.Requests.Query
{
    public class StudyCharacteristicsRequest : BaseQueryRequest
    {
        public string TitleContains { get; set; } = null!;

        public string LogicalOperator { get; set; } = null!;

        #nullable enable
        public string? TopicsInclude { get; set; }
    }
}