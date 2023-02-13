using Transaction.Api.Entities;

namespace Transaction.Api.Repositories
{
    public interface IRepository<T> where T: Transactions
    {
        Task<IReadOnlyCollection<T>> GetAllAsync(PagingParams pagingParams);
        Task<IReadOnlyCollection<T>> GetWithFilters(PagingParams pagingParams, QueryParams queryParams);
        Task CreateAsync(T entity);
        Task<T> GetAsync(Guid id);
    }
}