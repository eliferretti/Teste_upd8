using upd8.Domain.Enuns;

namespace upd8.Domain.Interfaces
{
    public interface IRepository<T, TId> where T : class
    {
        Task SaveAsync(T data);
        Task DeleteAsync(TId id);
        Task UpdateAsync(T data);   
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByFilterAsync(string cpf, string name, DateTime birthDate,
                               GenderEnum gender, string adress, string state,
                               string city);
    }
}