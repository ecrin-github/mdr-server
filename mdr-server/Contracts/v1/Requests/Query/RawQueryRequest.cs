using System.Collections.Generic;

namespace mdr_server.Contracts.v1.Requests.Query
{
    public class RawQueryRequest : BaseQueryRequest
    {
        public IDictionary<string, object> Query { get; set; }
    }
}