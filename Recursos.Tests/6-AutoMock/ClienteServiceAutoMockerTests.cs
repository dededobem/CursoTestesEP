using MediatR;
using Moq;
using Moq.AutoMock;
using Recursos.Clientes;
using Recursos.Tests._4_DadosHumanos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace Recursos.Tests._6_AutoMock
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServiceAutoMockerTests
    {
        private readonly ClienteTestsBogusFixture _clienteTestsBogusFixture;

        public ClienteServiceAutoMockerTests(ClienteTestsBogusFixture clienteTestsBogusFixture)
        {
            _clienteTestsBogusFixture = clienteTestsBogusFixture;
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            //Arrange
            var cliente = _clienteTestsBogusFixture.GerarClienteValido();
            var mock = new AutoMocker();

            var clienteService = mock.CreateInstance<ClienteServico>();

            //Act
            clienteService.Adicionar(cliente);

            //Assert
            Assert.True(cliente.EhValido()); //opcional
            mock.GetMock<IClienteRepositorio>().Verify(r => r.Adicionar(cliente), Times.Once);
            mock.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            //Arrange
            var cliente = _clienteTestsBogusFixture.GerarClienteInvalido();
            var mock = new AutoMocker();

            var clienteService = mock.CreateInstance<ClienteServico>();

            //Act
            clienteService.Adicionar(cliente);

            //Assert
            Assert.False(cliente.EhValido());
            mock.GetMock<IClienteRepositorio>().Verify(c => c.Adicionar(cliente), Times.Never);
            mock.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service AutoMock Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            //Arrange
            var mock = new AutoMocker();

            var clienteService = mock.CreateInstance<ClienteServico>();

            mock.GetMock<IClienteRepositorio>().Setup(r => r.ObterTodosAtivos())
                .Returns(_clienteTestsBogusFixture.GerarClientesVariados());

            //Act
            var clientes = clienteService.ObterTodosAtivos();

            //Assert
            mock.GetMock<IClienteRepositorio>().Verify(r => r.ObterTodosAtivos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(x => !x.Ativo) > 0);

        }
    }
}
