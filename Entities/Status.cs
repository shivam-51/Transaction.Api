using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Transaction.Api.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]  
    public enum Status
    {
        Other,
        Pending,
        Success,
        Failed,
        Cancelled,
        Refunded
    }
}