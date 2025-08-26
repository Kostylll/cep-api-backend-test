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
                ""logradouro"": ""Pra�a da S�"",
                ""complemento"": ""lado �mpar"",
                ""bairro"": ""S�"",
                ""localidade"": ""S�o Paulo"",
                ""uf"": ""SP"",
                ""ibge"": ""3550308"",
                ""gia"": ""1004"",
                ""ddd"": ""11"",
                ""siafi"": ""7107""
            }";

            var address = JsonConvert.DeserializeObject<Address>(json);

            Assert.NotNull(address);
            Assert.Equal("01001-000", address.Cep);
            Assert.Equal("Pra�a da S�", address.Logradouro);
            Assert.Equal("S�o Paulo", address.Localidade);
            Assert.Equal("SP", address.Uf);

        }
    }
}
