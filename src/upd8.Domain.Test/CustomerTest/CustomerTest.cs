using upd8.Domain.Entity;
using upd8.Domain.Enums;

namespace upd8.Domain.Test.CustomerTest
{
    public class CustomerTest
    {
        [Fact]
        public void upd8_CriaCustomer_RetornaCustomer()
        {
            var customerEsperado = new
            {
                Id = Guid.NewGuid().ToString(),
                Cpf = "99999999999",
                Name = "Eli Silva Ferretti Filho",
                BirthDate = DateTime.Now,
                Gender = GenderEnum.Masculino,
                Adress = "Rua dos Limadores - 483",
                State = "Rio de Janeiro",
                City = "RJ"
            };

            var customer = new Customer
            {
                Id = customerEsperado.Id,
                Name = customerEsperado.Name,
                Cpf = customerEsperado.Cpf,
                BirthDate= DateTime.Now,
                Gender= GenderEnum.Masculino,
                Adress= customerEsperado.Adress,
                State = customerEsperado.State,
                City = customerEsperado.City
            };

            Assert.Equal(customerEsperado.Id, customer.Id);
        }
    }
}
