using System.ComponentModel;
using upd8.Domain.Enums;

namespace upd8.Aplication.Dtos
{
    public class CustomerDto
    {
        public string Id { get; set; }
        [DisplayName("CPF")]
        public string Cpf { get; set; }
        [DisplayName("Nome")]
        public string Name { get; set; }
        [DisplayName("Data de nascimento")]
        public DateTime? BirthDate { get; set; }
        [DisplayName("Sexo")]
        public GenderEnum Gender { get; set; }
        [DisplayName("Endereco")]
        public string Adress { get; set; }
        [DisplayName("UF")]
        public string State { get; set; }
        [DisplayName("Cidade")]
        public string City { get; set; }
    }
}
