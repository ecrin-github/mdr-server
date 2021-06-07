using Nest;

namespace mdr_server.Entities.Object
{
    public class ObjectIdentifier
    {
        [Number(Name = "id")]
        public int? Id { get; set; }
        
        [Text(Name = "identifier_value")]
        public string IdentifierValue { get; set; }
        
        [Text(Name = "identifier_type")]
        public string IdentifierType { get; set; }
        
        [Date(Name = "identifier_date", Format = "YYYY MMM dd")]
        public string IdentifierDate { get; set; }
        
        [Text(Name = "identifier_org")]
        public string IdentifierOrg { get; set; }
    }
}