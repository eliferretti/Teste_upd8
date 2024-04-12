using AutoMapper;
using upd8.Aplication.Dtos;
using upd8.Aplication.Interfaces;
using upd8.Domain.Entity;
using upd8.Infrastructure.Interfaces;

namespace upd8.Aplication
{
    public class CustomerService : IGenericService<CustomerDto, AddCustomerDto, FilterCustomerDto, string>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper) 
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> AddAsync(AddCustomerDto data)
        {
            var customer = _mapper.Map<Customer>(data);
            try 
            {
                await _customerRepository.SaveAsync(customer);
                return _mapper.Map<CustomerDto>(customer);
            }
            catch 
            {
                throw new Exception("Add Error!");
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                await _customerRepository.DeleteAsync(id);
                return true;
            }
            catch 
            {
                throw new Exception("Delete Error!");
            }     
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            try
            {
                IEnumerable<Customer> customers = await _customerRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<CustomerDto>>(customers);
            }
            catch
            {
                throw new Exception("List Error!");
            }
        }

        public async Task<IEnumerable<CustomerDto>> GetByFilterAsync(FilterCustomerDto data)
        {
            try
            {
                var customer = _mapper.Map<Customer>(data);
                IEnumerable<Customer> customers = await _customerRepository.GetByFilterAsync(customer);
                return _mapper.Map<IEnumerable<CustomerDto>>(customers);
            }
            catch
            {
                throw new Exception("Find Error!");
            }
        }

        public async Task<CustomerDto> UpdateAsync(CustomerDto data)
        {
            try 
            {
                Customer customer = _mapper.Map<Customer>(data);
                await _customerRepository.UpdateAsync(customer);
                return _mapper.Map<CustomerDto>(customer);
            } 
            catch 
            {
                throw new Exception("Update Error!");
            }
        }
    }
}
