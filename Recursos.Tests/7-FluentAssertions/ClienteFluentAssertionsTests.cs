using FluentAssertions;
using Recursos.Tests._6_AutoMock;
using Xunit;
using Xunit.Abstractions;

namespace Recursos.Tests._7_FluentAssertions
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteFluentAssertionsTests
    {
        private readonly ClienteTestsAutoMockerFixture _clienteTestsAutoMockerFixture;
        private readonly ITestOutputHelper _testOutputHelper;

        public ClienteFluentAssertionsTests(ClienteTestsAutoMockerFixture clienteTestsAutoMockerFixture, 
            ITestOutputHelper testOutputHelper)
        {
            _clienteTestsAutoMockerFixture = clienteTestsAutoMockerFixture;
            _testOutputHelper = testOutputHelper;
        }

        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Cliente FluentAssertion Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            //Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteValido();

            //Act
            var result = cliente.EhValido();

            //Assert

            //Assert.True(result);
            //Assert.Equal(0, cliente.ValidationResult.Errors.Count);

            result.Should().BeTrue();
            cliente.ValidationResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente FluentAssertion Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            //Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteInvalido();

            //Act
            var result = cliente.EhValido();

            //Assert
            result.Should().BeFalse();
            cliente.ValidationResult.Errors.Should().HaveCountGreaterOrEqualTo(1);

            _testOutputHelper.WriteLine($"Foram encontrados {cliente.ValidationResult.Errors.Count} erros");

        }
    }
}
