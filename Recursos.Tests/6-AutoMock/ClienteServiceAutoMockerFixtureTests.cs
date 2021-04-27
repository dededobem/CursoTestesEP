using MediatR;
using Moq;
using Recursos.Clientes;
using System.Linq;
using System.Threading;
using Xunit;

namespace Recursos.Tests._6_AutoMock
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteServiceAutoMockerFixtureTests
    {
        private readonly ClienteTestsAutoMockerFixture _clienteTestsAutoMockerFixture;
        private readonly ClienteServico _clienteServico;

        public ClienteServiceAutoMockerFixtureTests(ClienteTestsAutoMockerFixture clienteTestsAutoMockerFixture)
        {
            _clienteTestsAutoMockerFixture = clienteTestsAutoMockerFixture;
            _clienteServico = _clienteTestsAutoMockerFixture.ObterClienteServico();
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service AutoMockFixture Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            //Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteValido();

            //Act
            _clienteServico.Adicionar(cliente);

            //Assert
            Assert.True(cliente.EhValido()); //opcional
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepositorio>().Verify(r => r.Adicionar(cliente), Times.Once);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service AutoMockFixture Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            //Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteInvalido();

            //Act
            _clienteServico.Adicionar(cliente);

            //Assert
            Assert.False(cliente.EhValido());
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepositorio>().Verify(c => c.Adicionar(cliente), Times.Never);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service AutoMockFixture Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            //Arrange

            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepositorio>().Setup(r => r.ObterTodosAtivos())
                .Returns(_clienteTestsAutoMockerFixture.GerarClientesVariados());

            //Act
            var clientes = _clienteServico.ObterTodosAtivos();

            //Assert
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepositorio>().Verify(r => r.ObterTodosAtivos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(x => !x.Ativo) > 0);
        }
    }
}
