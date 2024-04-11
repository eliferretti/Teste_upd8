using upd8.Domain.Enuns;
using System.ComponentModel.DataAnnotations.Schema;

namespace upd8.Domain.Entity
{
    [Table("Clientes")]
    public class Cliente : BaseEntity<string>
    {
        public Cliente() 
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Cpf { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate  { get; set; }
        public GenderEnum Gender { get; set; }
        public string Adress { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}
