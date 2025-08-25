using CepApi.Application.Abstraction.Domain.DTO;
using CepApi.Application.Abstraction.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CepApi.Application.Interfaces
{
    public interface ICepServices
    {
        Task<AddressDTO> GetCep(String cep);

        Task<List<Address>> GetAllAdresses(int page, int pageSize);

        Task<IEnumerable<AddressDTO>> GetByCityAsync(String city);
    }
}
