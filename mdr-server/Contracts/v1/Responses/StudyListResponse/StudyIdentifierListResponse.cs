using mdr_server.Entities.Common;

namespace mdr_server.Contracts.v1.Responses.StudyListResponse
{
    public class StudyIdentifierListResponse
    {
        #nullable enable
        public int? Id { get; set; }
        
        public string? IdentifierValue { get; set; }
        
        public string? IdentifierType { get; set; }
        
        public string? IdentifierDate { get; set; }
        
        public string? IdentifierLink { get; set; }
        
        public IdentifierOrg? IdentifierOrg { get; set; }
    }
}