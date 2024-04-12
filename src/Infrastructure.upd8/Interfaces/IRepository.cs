using upd8.Domain.Enums;

namespace upd8.Domain.Interfaces
{
    public interface IRepository<T, TId> where T : class
    {
        Task<T> GetSingleAsync(TId id);
        Task SaveAsync(T data);
        Task DeleteAsync(TId id);
        Task UpdateAsync(T data);   
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByFilterAsync(T data);
    }
}