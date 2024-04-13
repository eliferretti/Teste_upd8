using upd8.Domain.Enums;

namespace upd8.Aplication.Dtos
{
    public class FilterCustomerDto
    {
        public string? Id { get; set; }
        public string? Cpf { get; set; }
        public string? Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderEnum? Gender { get; set; }
        public string? Adress { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
    }
}
