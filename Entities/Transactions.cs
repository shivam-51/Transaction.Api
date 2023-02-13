using System;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MongoDB.Bson;


namespace Transaction.Api.Entities
{
    public class Transactions: IEntity
    {
        public Guid Id { get; set; }

        public Guid StoreId {get; set;}

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; } = Status.Pending;

        public string? NameOfMerchant { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}

