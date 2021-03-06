using MediatR;
using Moq;
using Recursos.Clientes;
using Recursos.Tests._4_DadosHumanos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace Recursos.Tests._5_Mock
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServiceTests
    {
        private readonly ClienteTestsBogusFixture _clienteTestsBogusFixture;

        public ClienteServiceTests(ClienteTestsBogusFixture clienteTestsBogusFixture)
        {
            _clienteTestsBogusFixture = clienteTestsBogusFixture;
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            //Arrange
            var cliente = _clienteTestsBogusFixture.GerarClienteValido();
            var clienteRepo = new Mock<IClienteRepositorio>();
            var mediatr = new Mock<IMediator>();

            var clienteService = new ClienteServico(clienteRepo.Object, mediatr.Object);

            //Act
            clienteService.Adicionar(cliente);

            //Assert
            Assert.True(cliente.EhValido()); //opcional
            clienteRepo.Verify(r => r.Adicionar(cliente), Times.Once);
            mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            //Arrange
            var cliente = _clienteTestsBogusFixture.GerarClienteInvalido();
            var clienteRepo = new Mock<IClienteRepositorio>();
            var mediatr = new Mock<IMediator>();

            var clienteService = new ClienteServico(clienteRepo.Object, mediatr.Object);

            //Act
            clienteService.Adicionar(cliente);

            //Assert
            Assert.False(cliente.EhValido());
            clienteRepo.Verify(c => c.Adicionar(cliente), Times.Never);
            mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            //Arrange
            var clienteRepo = new Mock<IClienteRepositorio>();
            var mediatr = new Mock<IMediator>();

            clienteRepo.Setup(r => r.ObterTodosAtivos())
                .Returns(_clienteTestsBogusFixture.GerarClientesVariados());

            var clienteServico = new ClienteServico(clienteRepo.Object, mediatr.Object);

            //Act
            var clientes = clienteServico.ObterTodosAtivos();

            //Assert
            clienteRepo.Verify(r => r.ObterTodosAtivos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(x => !x.Ativo) > 0);
        }
    }
}
