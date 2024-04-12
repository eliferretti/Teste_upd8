using upd8.Aplication.Dtos;

namespace upd8.MVC.Models
{
    public class CustomerModel
    {
        public CustomerDto? Customer { get; set; }
        public IEnumerable<CustomerDto>? Customers { get; set; }
        public IEnumerable<Estado> Estados { get; set; } = Enumerable.Empty<Estado>();
    }

    public class Regiao
    {
        public int Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
    }

    public class Estado
    {
        public int Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public Regiao Regiao { get; set; }
    }
}
