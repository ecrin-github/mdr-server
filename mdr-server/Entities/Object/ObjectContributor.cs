using Nest;

namespace mdr_server.Entities.Object
{
    public class ObjectContributor
    {
        [Number(Name = "id")]
        public int? Id { get; set; }
        
        [Text(Name = "contribution_type")]
        public string ContributionType { get; set; }
        
        [Boolean(Name = "is_individual")]
        public bool IsIndividual { get; set; }
        
        [Text(Name =  "organisation")]
        public string Organisation { get; set; }
        
        [Object]
        [PropertyName("person")]
        public Person Person { get; set; }
    }
}