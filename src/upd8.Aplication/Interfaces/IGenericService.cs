namespace upd8.Aplication.Interfaces
{
    public interface IGenericService<TDto, TAddDto, TFDto, TId>
    {
        Task<IEnumerable<TDto>> GetByFilterAsync(TFDto data);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<bool> DeleteAsync(string id);
        Task<TDto> UpdateAsync(TDto data);
        Task<TDto> AddAsync(TAddDto data);
        Task<TDto> GetSingleAsync(string id);
    }
}
