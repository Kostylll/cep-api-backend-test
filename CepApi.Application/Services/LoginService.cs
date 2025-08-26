using AutoMapper;
using CepApi.Application.Abstraction.Domain.DTO;
using CepApi.Application.Abstraction.Domain.Models;
using CepApi.Application.Infra.Data.Context;
using CepApi.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CepApi.Application.Services
{
    public class LoginService : ILoginServices
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly CepApiDbContext _context;
        public LoginService(IConfiguration config, IMapper mapper, CepApiDbContext context) {
        
            _config = config;
            _mapper = mapper;
            _context = context;
         
       
        }

        public async Task<LoginDTO> CreateUser(LoginDTO login)
        {

            var exists = await _context.Login.AnyAsync(u => u.Email == login.Email);
            if (exists) 
                throw new Exception("Já existe um usuário com este e-mail");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(login.Password);

            var entity = _mapper.Map<Login>(login);
            entity.Password = hashedPassword;

            await _context.Login.AddAsync(entity);
            await _context.SaveChangesAsync();


            return _mapper.Map<LoginDTO>(entity);
        }

        public async Task<TokenDTO> LoginAsync(LoginDTO login)
        {
            if (login == null)
                throw new ArgumentNullException(nameof(login));

            var entity = await _context.Login.FirstOrDefaultAsync(u => u.Email == login.Email);
            var decodedPassword = BCrypt.Net.BCrypt.Verify(login.Password, entity.Password);

            if (entity == null)
                throw new Exception("Usuário não encontrado.");

            if (!decodedPassword) 
                throw new Exception("Senha inválida.");


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Sub, login.Email),
               new Claim(ClaimTypes.Role, "Admin")
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            var jwt = tokenHandler.WriteToken(token);

            return new TokenDTO
            {
                Token = jwt,
                Expiration = token.ValidTo
            };
        }
    }
}
