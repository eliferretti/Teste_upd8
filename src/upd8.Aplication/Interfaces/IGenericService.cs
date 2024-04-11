namespace upd8.Aplication.Interfaces
{
    public interface IGenericService<TDto, TAddDto, TId>
    {
        Task<IEnumerable<TDto>> GetByFilterAsync(TDto data);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<bool> DeleteAsync(string id);
        Task<TDto> UpdateAsync(TDto data);
        Task<TDto> AddAsync(TAddDto data);
    }
}
