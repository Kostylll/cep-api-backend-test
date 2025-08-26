using Newtonsoft.Json;
using CepApi.Application.Abstraction.Domain.Models;

namespace CepApi.Application.UnitTests
{
    public class CepUnitTest
    {
        [Fact]
        public void DeveDesserializarJsonDeCepCorretamente()
        {

            var json = @"{
                ""cep"": ""01001-000"",
                ""logradouro"": ""Praça da Sé"",
                ""complemento"": ""lado ímpar"",
                ""bairro"": ""Sé"",
                ""localidade"": ""São Paulo"",
                ""uf"": ""SP"",
                ""ibge"": ""3550308"",
                ""gia"": ""1004"",
                ""ddd"": ""11"",
                ""siafi"": ""7107""
            }";

            var address = JsonConvert.DeserializeObject<Address>(json);

            Assert.NotNull(address);
            Assert.Equal("01001-000", address.Cep);
            Assert.Equal("Praça da Sé", address.Logradouro);
            Assert.Equal("São Paulo", address.Localidade);
            Assert.Equal("SP", address.Uf);

        }
    }
}
