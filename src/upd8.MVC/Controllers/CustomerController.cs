using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using upd8.Aplication.Dtos;
using upd8.Infrastructure.Interfaces;
using upd8.MVC.Models;

namespace upd8.MVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IClientFactory _clientFactory;

        public CustomerController(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            CustomerModel model = new();
            var url = "https://localhost:7220/api/v1/Customer";
            model.Customers = await _clientFactory.Get<IEnumerable<CustomerDto>>(url);
            model.Estados = await GetEstados();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CustomerModel model)
        {
           
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            CustomerModel model = new();
            model.Estados = await GetEstados();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerModel model)
        {
            var url = "https://localhost:7220/api/v1/Customer";
            var content = new StringContent(JsonConvert.SerializeObject(model.Customer), Encoding.UTF8, "application/json");
            model.Customer = await _clientFactory.Post<CustomerDto>(url, content);
            TempData["msg"] = "Cliente cadastrdo com sucesso!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = new CustomerModel();
            var url = $"https://localhost:7220/api/v1/Customer/{id}";
            model.Customer = await _clientFactory.Get<CustomerDto>(url);
            model.Estados = await GetEstados();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerModel model)
        {
            var url = $"https://localhost:7220/api/v1/Customer/{model.Customer.Id}";
            var oldCstomer = await _clientFactory.Get<CustomerDto>(url);
            
            if(model.Customer.City == null)
                model.Customer.City = oldCstomer.City;

            url = "https://localhost:7220/api/v1/Customer";
            var content = new StringContent(JsonConvert.SerializeObject(model.Customer), Encoding.UTF8, "application/json");
            var result = await _clientFactory.Update<CustomerDto>(url, content);
            return RedirectToAction("Index");
        }

        public async Task<IEnumerable<Estado>> GetEstados() 
        {
            var url = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";
            var estados = await _clientFactory.Get<IEnumerable<Estado>>(url);
            return estados.OrderBy(x => x.Sigla);
        }

        [HttpGet]
        public async Task<IActionResult> ObterCidadesPorEstado(int estadoId)
        {
            var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{estadoId}/municipios";
            var response = await _clientFactory.Get<IEnumerable<Cidade>>(url);
            return Json(response);
        }

        public class Cidade
        {
            public int Id { get; set; }
            public string Nome { get; set; }
        }
    }
}
