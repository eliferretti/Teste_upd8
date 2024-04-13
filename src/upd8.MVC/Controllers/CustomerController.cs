using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using upd8.Aplication.Dtos;
using upd8.Infrastructure.Interfaces;
using upd8.Infrastructure.Services;
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
            try 
            {
                var url = "https://localhost:7220/api/v1/Customer";
                var content = new StringContent(JsonConvert.SerializeObject(model.Customer), Encoding.UTF8, "application/json");
                model.Customer = await _clientFactory.Post<CustomerDto>(url, content);
                if (model.Customer.Id != null)
                    TempData["msgSuccess"] = "Cliente cadastrdo com sucesso!";
                else
                    TempData["msgError"] = "Erro ao tentar cadastrar";
            }catch (Exception ex) 
            {
                TempData["msgError"] = $"Erro ao tentar cadastrar. {ex.Message}";
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var model = new CustomerModel();
                var url = $"https://localhost:7220/api/v1/Customer/{id}";
                model.Customer = await _clientFactory.Get<CustomerDto>(url);
                model.Estados = await GetEstados();
                return View(model);
            }
            catch (Exception ex) 
            {
                TempData["msgError"] = $"Erro ao tentar cadastrar. {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerModel model)
        {
            try
            {
                var url = $"https://localhost:7220/api/v1/Customer/{model.Customer.Id}";
                var oldCstomer = await _clientFactory.Get<CustomerDto>(url);

                if (model.Customer.City == null)
                    model.Customer.City = oldCstomer.City;

                url = "https://localhost:7220/api/v1/Customer";
                var content = new StringContent(JsonConvert.SerializeObject(model.Customer), Encoding.UTF8, "application/json");
                var result = await _clientFactory.Update<CustomerDto>(url, content);
                if (result != null)
                    TempData["msgSuccess"] = "Cliente editado com sucesso!";
                else
                    TempData["msgError"] = "Erro ao tentar editar";
            }
            catch (Exception ex)
            {
                TempData["msgError"] = $"Erro ao tentar editar. {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Filter(CustomerModel model)
        {
            try
            {
                var url = "https://localhost:7220/api/v1/Customer/filter";
                var content = new StringContent(JsonConvert.SerializeObject(model.Customer), Encoding.UTF8, "application/json");
                CustomerModel filterModel = new CustomerModel();
                filterModel.Customers = await _clientFactory.Post<IEnumerable<CustomerDto>>(url, content);
                filterModel.Estados = await GetEstados();
                return View("Index", filterModel);
            }
            catch (Exception ex)
            {
                TempData["msgError"] = $"Erro ao tentar filtrar. {ex.Message}";
            }
            return View("Index", model);
        }

        public async Task<IActionResult> Delete(string id) 
        {
            try 
            {
                var url = $"https://localhost:7220/api/v1/Customer/{id}";
                var result = await _clientFactory.Delete(url);
                if (result != null)
                    TempData["msgSuccess"] = "Cliente excluido com sucesso!";
                else
                    TempData["msgError"] = "Erro ao tentar excluir";
            }
            catch (Exception ex)
            {
                TempData["msgError"] = $"Erro ao tentar excluir. {ex.Message}";
            }
     
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
