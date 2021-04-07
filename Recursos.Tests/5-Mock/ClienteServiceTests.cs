using MediatR;
using Moq;
using Recursos.Clientes;
using Recursos.Tests._4_DadosHumanos;
using System;
using System.Collections.Generic;
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
        public void ClenteService_Adicionar_DeveExecutarComSucesso()
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
        public void ClenteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {

        }
    }
}
