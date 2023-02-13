using System;
using System.Collections;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Transaction.Api.Entities;

namespace Transaction.Api.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T: IEntity
    {
        private readonly IMongoCollection<T> dbCollection;

        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        public MongoRepository(
        IMongoDatabase mongoDatabase, string collectionName)
        {
            //const string collectionName = "items";
            //Console.WriteLine(mongoDbSettings.Value.Host);
            //Console.WriteLine(mongoDbSettings.Value.Port);
            //var mongoClient = new MongoClient(
            //    mongoDbSettings.Value.ConnectionString);

            //var mongoDatabase = mongoClient.GetDatabase(
            //    mongoDbSettings.Value.DatabaseName);

            dbCollection = mongoDatabase.GetCollection<T>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            var items = await dbCollection.Find(filterBuilder.Empty).ToListAsync();
            Console.WriteLine(items);
            return items;
        }

        // public async Task<T> GetAsync(Guid id)
        // {
        //     FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);

        //     return await dbCollection.Find(filter).SingleOrDefaultAsync();
        // }

        public async Task CreateAsync(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
                // return null;
            }
            await dbCollection.InsertOneAsync(entity);
            // return entity;
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await dbCollection.Find(filterBuilder.Eq(entity => entity.Id, id)).SingleOrDefaultAsync();
        }
    }
}

