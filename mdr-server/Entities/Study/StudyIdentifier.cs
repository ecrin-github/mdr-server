using mdr_server.Entities.Common;
using Nest;

namespace mdr_server.Entities.Study
{
    public class StudyIdentifier
    {
        [Number(Name = "id")]
        public int Id { get; set; }
        
        [Text(Name = "identifier_value")]
        public string IdentifierValue { get; set; }
        
        [Object]
        [PropertyName("identifier_type")]
        public IdentifierType IdentifierType { get; set; }
        
        [Date(Name = "identifier_date", Format = "yyyy MMM dd")]
        public string IdentifierDate { get; set; }
        
        [Text(Name = "identifier_link")]
        public string IdentifierLink { get; set; }
        
        [Object]
        [PropertyName("identifier_org")]
        public IdentifierOrg IdentifierOrg { get; set; }
    }
}