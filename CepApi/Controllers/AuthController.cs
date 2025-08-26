using CepApi.Application.Abstraction.Domain.DTO;
using CepApi.Application.Interfaces;
using CepApi.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CepApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly ILoginServices _loginServices;
        public AuthController(IConfiguration config, ILoginServices loginServices) {

            _config = config;
            _loginServices = loginServices;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                return BadRequest("Email e senha são obrigatórios.");

            var result = await _loginServices.LoginAsync(login);

            return Ok(result);
        }

        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] LoginDTO login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                return BadRequest("Email e senha são obrigatórios.");

            var result = await _loginServices.CreateUser(login);
            return Ok(result);
        }
    }
}
