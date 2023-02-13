using Transaction.Api.Entities;

namespace Transaction.Api.Repositories
{
    public interface IRepository<T> where T: IEntity
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task CreateAsync(T entity);
        Task<T> GetAsync(Guid id);
    }
}