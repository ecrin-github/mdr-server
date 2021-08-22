using System.Collections.Generic;

namespace mdr_server.Entities.FetchedData
{
    public class FetchedStudies
    {
        public long Total { get; set; }
        public ICollection<Study.Study> Studies { get; set; }
    }
}