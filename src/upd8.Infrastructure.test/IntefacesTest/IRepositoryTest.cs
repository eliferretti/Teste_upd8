using Moq;
using upd8.Domain.Entity;
using upd8.Domain.Enums;
using upd8.Domain.Interfaces;

namespace upd8.Infrastructure.test.IntefacesTest
{
    public class IRepositoryTest
    {
        private readonly Mock<IRepository<Customer, string>> _repositoryMock;

        public IRepositoryTest()
        {
            _repositoryMock = new Mock<IRepository<Customer, string>>();
        }

        [Fact]
        public async Task GetSingleAsync_ReturnsEntity()
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid().ToString(),
                Cpf = "9999999999",
                Name = "Eli Ferretti",
                BirthDate = DateTime.Now,
                Gender = GenderEnum.Masculino,
                Adress = "Rua dos Limadores",
                State = "RJ",
                City = "Rio de Janeiro"
            };

            _repositoryMock.Setup(repo => repo.GetSingleAsync(customer.Id)).ReturnsAsync(customer);
        
            var result = await _repositoryMock.Object.GetSingleAsync(customer.Id);

            Assert.NotNull(result);
            Assert.Equal(customer.Id, result.Id);
            Assert.Equal("Eli Ferretti", result.Name);
        }

        [Fact]
        public async Task SaveAsync_CallsSaveMethod()
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid().ToString(),
                Cpf = "9999999999",
                Name = "Eli Ferretti",
                BirthDate = DateTime.Now,
                Gender = GenderEnum.Masculino,
                Adress = "Rua dos Limadores",
                State = "RJ",
                City = "Rio de Janeiro"
            };

            await _repositoryMock.Object.SaveAsync(customer);

            _repositoryMock.Verify(repo => repo.SaveAsync(customer), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsDeleteMethod()
        {
            var customerId = Guid.NewGuid().ToString();

            await _repositoryMock.Object.DeleteAsync(customerId);

            _repositoryMock.Verify(repo => repo.DeleteAsync(customerId), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CallsUpdateMethod()
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid().ToString(),
                Cpf = "9999999999",
                Name = "Eli Ferretti",
                BirthDate = DateTime.Now,
                Gender = GenderEnum.Masculino,
                Adress = "Rua dos Limadores",
                State = "RJ",
                City = "Rio de Janeiro"
            };

            await _repositoryMock.Object.UpdateAsync(customer);

            _repositoryMock.Verify(repo => repo.UpdateAsync(customer), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsEntities()
        {
            var testCustomers = new List<Customer>
            {
                new Customer
                {
                    Id = Guid.NewGuid().ToString(),
                    Cpf = "9999999999",
                    Name = "Eli Ferretti",
                    BirthDate = DateTime.Now,
                    Gender = GenderEnum.Masculino,
                    Adress = "Rua dos Limadores",
                    State = "RJ",
                    City = "Rio de Janeiro"
                },
                 new Customer
                {
                    Id = Guid.NewGuid().ToString(),
                    Cpf = "12312312123",
                    Name = "Leticia Richa",
                    BirthDate = DateTime.Now,
                    Gender = GenderEnum.Feminino,
                    Adress = "Rua dos Limadores",
                    State = "RJ",
                    City = "Rio de Janeiro"
                },
            };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(testCustomers);

            var result = await _repositoryMock.Object.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, ((List<Customer>)result).Count);
        }

        [Fact]
        public async Task GetByFilterAsync_ReturnsFilteredEntities()
        {
            // Arrange
            var filterCustomer = new Customer { Name = "Filtered Name" };
            var testCustomers = new List<Customer>
            {
                new Customer { Id = Guid.NewGuid().ToString(), Name = "Filtered Name" }
            };

            _repositoryMock.Setup(repo => repo.GetByFilterAsync(filterCustomer)).ReturnsAsync(testCustomers);

            // Act
            var result = await _repositoryMock.Object.GetByFilterAsync(filterCustomer);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

    }
}
