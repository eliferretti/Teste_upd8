using Microsoft.EntityFrameworkCore;
using upd8.API.Middlewares;
using upd8.Aplication;
using upd8.Aplication.Dtos;
using upd8.Aplication.Helpers;
using upd8.Aplication.Interfaces;
using upd8.Infrastructure.Data;
using upd8.Infrastructure.Interfaces;
using upd8.Infrastructure.Repositories;
using upd8.Infrastructure.Services;

namespace upd8.API.Extencions
{
    public static class IoCExtencion
    {
        public static void AddIoC(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<DataContext>(
              opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<ErrorHandlerMiddleware>();
            services.AddScoped<IGenericService<CustomerDto, AddCustomerDto, string>, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IClientFactory, HttpClientService>();
            services.AddHttpClient();

            services.AddAutoMapper(typeof(CustomerMapProfile));
        }
    }
}
