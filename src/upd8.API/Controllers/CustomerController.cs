using Microsoft.AspNetCore.Mvc;
using upd8.Aplication.Dtos;
using upd8.Aplication.Interfaces;

namespace upd8.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IGenericService<CustomerDto, AddCustomerDto, string> _genericService;
        public CustomerController(IGenericService<CustomerDto, AddCustomerDto, string> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<CustomerDto> customers = await _genericService.GetAllAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"List Error. Message:{ex.Message}");
            }
        }
    }
}
