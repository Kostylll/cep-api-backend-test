using CepApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CepApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViaCepController : Controller
    {
        private readonly ICepServices _cepServices;

        public ViaCepController(ICepServices cepServices)
        {
            _cepServices = cepServices;
        }

        [HttpGet("{cep}")]
        public async Task<IActionResult> GetCep(string cep)
        {
            var address = await _cepServices.GetCep(cep);

            if (address == null)
                return NoContent();

            return Ok(address);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var addresses = await _cepServices.GetAllAdresses(page, pageSize);

            if (addresses == null || !addresses.Any())
                return NoContent();

            return Ok(addresses);
        }


        [HttpGet("city")]
        public async Task<IActionResult> GetByCity(string city)
        {
            var addresses = await _cepServices.GetByCityAsync(city);

            if (!addresses.Any())
                return NotFound($"No addresses found for city '{city}'.");

            return Ok(addresses);

        }
    }
}
