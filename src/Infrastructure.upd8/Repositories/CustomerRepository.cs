using upd8.Domain.Entity;
using upd8.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using upd8.Infrastructure.Interfaces;
using upd8.Domain.Enums;

namespace upd8.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository // IRepository<Customer, string>
    {
        private DataContext _context { get; init; }
        public CustomerRepository(DataContext context) 
        {
            _context = context;
        }

        public async Task DeleteAsync(string id)
            => await _context.Customers.Where(x => x.Id == id).ExecuteDeleteAsync();


        public async Task<IEnumerable<Customer>> GetAllAsync()
             => await _context.Customers.ToListAsync();

        public async Task SaveAsync(Customer data)
        {
            await _context.Customers.AddAsync(data);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(Customer data)
        {
            _context.Customers.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetByFilterAsync(Customer data)
        {
            IQueryable<Customer> query = _context.Customers;

            if(!string.IsNullOrEmpty(data.Cpf))
                query = query.Where(c => c.Cpf == data.Cpf);

            if(!string.IsNullOrEmpty(data.Name))
                query = query.Where(c => c.Name == data.Name);

            if(data.BirthDate != null)
                query = query.Where(c => c.BirthDate == data.BirthDate);
            
            if(data.Gender == GenderEnum.Masculino)
                query = query.Where(c => c.Gender == data.Gender);
            else if(data.Gender == GenderEnum.Feminino)
                query = query.Where(c => c.Gender == data.Gender);

            if (!string.IsNullOrEmpty(data.Adress))
                query = query.Where(c => c.Adress == data.Adress);

            if(!string.IsNullOrEmpty(data.State))
                query = query.Where(c => c.State == data.State);

            if(!string.IsNullOrEmpty(data.City))
                query = query.Where(c => c.City == data.City);

            return await query.ToListAsync();
        }

        public async Task<Customer> GetSingleAsync(string id)
            => await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
    }
}
