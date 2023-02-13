using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Transaction.Api.Entities;
// using Transaction.Api.Settings;
using Microsoft.Extensions.Options;

namespace Transaction.Api.Repositories
{
	public static class Extensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
		{
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddSingleton(provider =>
            {
                //var configuration = provider.GetRequiredService<IConfiguration>();
                //var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoDbSettings = provider.GetRequiredService<IOptions<MongoDbSettings>>();
                Console.WriteLine(mongoDbSettings.Value.ConnectionString);
                try
                {
                    MongoClient mongoClient = new MongoClient("mongodb+srv://sc_admin:ADMIN12345@cluster.sdurkt7.mongodb.net?authSource=admin");
                    return mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error connecting to MongoDB: " + e.Message);
                    return null;
                }
            });
            return services;
        }
        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName) where T : IEntity
        {


            services.AddSingleton<IRepository<T>>(provider =>
            {
                var database = provider.GetService<IMongoDatabase>();
                return new MongoRepository<T>(database, collectionName);
            });
            return services;
        } 
    }
}

