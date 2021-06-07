using Nest;

namespace mdr_server.Entities.Object
{
    public class DatasetRecordKeys
    {
        [Text(Name = "keys_type")]
        public string KeysType { get; set; }
        
        [Text(Name = "keys_details")]
        public string KeysDetails { get; set; }
    }
}