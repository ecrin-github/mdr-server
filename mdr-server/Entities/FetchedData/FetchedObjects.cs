using System.Collections.Generic;

namespace mdr_server.Entities.FetchedData
{
    public class FetchedObjects
    {
        public long Total { get; set; }
        public ICollection<Object.Object> Objects { get; set; }
    }
}