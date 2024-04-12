using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using upd8.Aplication.Dtos;
using upd8.Infrastructure.Interfaces;
using upd8.MVC.Models;

namespace upd8.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClientFactory _clientFactory;

        public HomeController(ILogger<HomeController> logger, IClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            CustomerModel model = new();
            var url = "https://localhost:7220/api/v1/Customer";
            model.Customers = await _clientFactory.Get<IEnumerable<CustomerDto>>(url);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}