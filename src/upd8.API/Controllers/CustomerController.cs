using Microsoft.AspNetCore.Mvc;
using upd8.Aplication.Dtos;
using upd8.Aplication.Interfaces;

namespace upd8.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IGenericService<CustomerDto, AddCustomerDto, FilterCustomerDto, string> _genericService;
        public CustomerController(IGenericService<CustomerDto, AddCustomerDto, FilterCustomerDto, string> genericService)
        {
            _genericService = genericService;
        }
        
        [HttpPost("filter")]
        public async Task<ActionResult> GetByFilterAsync(FilterCustomerDto data)
        {
            try
            {
                IEnumerable<CustomerDto> customers = await _genericService.GetByFilterAsync(data);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"List Error. Message:{ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
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

        [HttpPost]
        public async Task<IActionResult> PostAsync(AddCustomerDto data)
        {
            try
            {
                var customer = await _genericService.AddAsync(data);
                if (customer == null) return NoContent();
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Post Error. Message:{ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(CustomerDto data) 
        {
            try
            {
                var customer = await _genericService.UpdateAsync(data);
                if (customer == null) return NoContent();
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Update Error. Message:{ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id) 
        {
            try 
            {
                var response = await _genericService.DeleteAsync(id);
                if (response == false) return NoContent();
                return Ok(new { message = "Deleted"});
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Delete Error! Message:{ex.Message}");
            }
        }
    }
}
