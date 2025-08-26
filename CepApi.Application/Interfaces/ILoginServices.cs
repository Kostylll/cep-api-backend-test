using CepApi.Application.Abstraction.Domain.DTO;


namespace CepApi.Application.Interfaces
{
    public interface ILoginServices
    {
        Task<LoginDTO> CreateUser(LoginDTO login);
        Task<TokenDTO> LoginAsync(LoginDTO login);
    }
}
