using System;

namespace Transaction.Api.Entities
{
    public class MongoDbSettings
    {
        public string? Host { get; set; }

        public string? Port { get; set; }

        public string DatabaseName => "dotnet";

        public string ConnectionString => $"mongodb://{this.Host}";
        //public string ConnectionString { get; set; } = null!;
    }
}