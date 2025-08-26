using AutoMapper;
using CepApi.Application.Abstraction.Domain.DTO;
using CepApi.Application.Abstraction.Domain.Models;
using CepApi.Application.Infra.Data.Context;
using CepApi.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CepApi.Application.Services
{
    public class CepService : ICepServices
    {

        private readonly CepApiDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly IMapper _mapper;

        public CepService(HttpClient httpClient, IConfiguration configuration, CepApiDbContext context, IMapper mapper)
        {

            _httpClient = httpClient;
            _baseUrl = configuration["ViaCep:BaseUrl"] ?? throw new ArgumentNullException(nameof(configuration));
            _context = context;
            _mapper = mapper;
        }

        public async Task<AddressDTO> GetCep(string cep)
        {

            if (string.IsNullOrEmpty(cep))
                throw new ArgumentException("Cep Inválido.", nameof(cep));

            var existing = await _context.Address.AsNoTracking().FirstOrDefaultAsync(a => a.Cep == cep);

            if (existing != null)
                return _mapper.Map<AddressDTO>(existing);

            var httpResponse = await _httpClient.GetAsync($"{_baseUrl}/{cep}/json/");
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Erro ao consultar ViaCEP: {httpResponse.StatusCode}");

            }

            var address = await httpResponse.Content.ReadFromJsonAsync<Address>();

            if (address == null || string.IsNullOrEmpty(address.Cep))
                return null;

            await _context.Address.AddAsync(address);
            await _context.SaveChangesAsync();

            return _mapper.Map<AddressDTO>(address);

        }


        public async Task<List<Address>> GetAllAdresses(int page = 1, int pageSize = 20)
        {

            if (pageSize <= 0) pageSize = 1;
            if (pageSize <= 0 || pageSize > 100) pageSize = 20;

            var query = _context.Address.AsNoTracking().OrderBy(a => a.Id);

            var addresses = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return addresses;

        }

        public async Task<IEnumerable<AddressDTO>> GetByCityAsync(string city)
        {

            if (string.IsNullOrEmpty(city))
                return new List<AddressDTO>();

            var addresses = await _context.Set<Address>()
                                       .Where(a => !string.IsNullOrEmpty(a.Localidade) &&
                                                   a.Localidade.ToLower().Contains(city.Trim().ToLower()))
                                       .ToListAsync();

            var result = _mapper.Map<IEnumerable<AddressDTO>>(addresses);

            return result;

        }
    }
}

