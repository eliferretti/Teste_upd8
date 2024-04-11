using upd8.Domain.Entity;
using upd8.Domain.Enuns;
using upd8.Domain.Interfaces;
using upd8.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace upd8.Infrastructure.Repositories
{
    public class ClienteRepository : IRepository<Cliente, string>
    {
        private DataContext _context { get; init; }
        public ClienteRepository(DataContext context) 
        {
            _context = context;
        }

        public async Task DeleteAsync(string id)
            => await _context.Clientes.Where(x => x.Id == id).ExecuteDeleteAsync();


        public async Task<IEnumerable<Cliente>> GetAllAsync()
             => await _context.Clientes.ToListAsync();

        public async Task SaveAsync(Cliente data)
        {
            await _context.Clientes.AddAsync(data);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Cliente data)
        {
            _context.Clientes.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cliente>> GetByFilterAsync(string cpf, string name, DateTime birthDate, 
                                                                 GenderEnum gender, string adress, string state, 
                                                                 string city)
        {
            return await _context.Clientes.Where(c => c.Cpf == cpf || c.Name == name || c.BirthDate == birthDate ||
                                                      c.Gender == gender || c.Adress == adress || c.State == state ||
                                                      c.City == city).ToListAsync();
        }
    }
}
