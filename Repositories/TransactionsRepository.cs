using System;
using System.Collections;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Transaction.Api.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Transaction.Api.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T: Transactions
    {
        private readonly IMongoCollection<T> dbCollection;

        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        public MongoRepository(
        IMongoDatabase mongoDatabase, string collectionName)
        {
            dbCollection = mongoDatabase.GetCollection<T>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(PagingParams pagingParams)
        {
            // var filter = filterBuilder.And(
            //     filterBuilder.Eq("NameOfMerchant", "Flipkart")
            // );

            var items = await dbCollection.Find(filterBuilder.Empty).Skip((pagingParams.PageNumber - 1)*pagingParams.PageSize).Limit(pagingParams.PageSize).ToListAsync();

            return items;
        }

        public async Task<IReadOnlyCollection<T>> GetWithFilters(PagingParams pagingParams, QueryParams queryParams)
        {
            var filter = Builders<T>.Filter.Where(x => 
            (x.StoreId == queryParams.StoreId || queryParams.StoreId == Guid.Empty) 
            && (x.NameOfMerchant == queryParams.NameOfMerchant || queryParams.NameOfMerchant == "") 
            && (x.Status == queryParams.Status || queryParams.Status == Status.Other) 
            && (x.Price >= queryParams.PriceFrom || queryParams.PriceFrom == Decimal.MinValue) 
            && (x.Price <= queryParams.PriceTo || queryParams.PriceTo == Decimal.MaxValue) 
            && (x.CreatedTime >= queryParams.FromDate || queryParams.FromDate == DateTime.MinValue) 
            && (x.CreatedTime <= queryParams.ToDate || queryParams.ToDate == DateTime.MaxValue));
            
            var items = await dbCollection.Find(filter).Skip((pagingParams.PageNumber - 1)*pagingParams.PageSize).Limit(pagingParams.PageSize).ToListAsync();

            return items;
        }


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

