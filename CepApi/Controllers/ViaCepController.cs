using CepApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [Authorize(Roles = "Admin")]
        [HttpGet("{cep}")]
        [SwaggerOperation(Summary = "Returns CEP Info",Description = "Get the informations about the CEP you put")]
        public async Task<IActionResult> GetCep([FromRoute] string cep)
        {
            var address = await _cepServices.GetCep(cep);

            if (address == null)
                return NoContent();

            return Ok(address);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [SwaggerOperation(Summary = "Returns all CEP", Description = "Returns CEP search history")]
        public async Task<IActionResult> GetAllAddresses([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var addresses = await _cepServices.GetAllAdresses(page, pageSize);

            if (addresses == null || !addresses.Any())
                return NoContent();

            return Ok(addresses);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("city")]
        [SwaggerOperation(Summary = "Return by City",Description = "Return CEP seatch history by city")]
        public async Task<IActionResult> GetByCity([FromQuery] string city)
        {
            var addresses = await _cepServices.GetByCityAsync(city);

            if (!addresses.Any())
                return NotFound($"No addresses found for city '{city}'.");

            return Ok(addresses);

        }
    }
}
