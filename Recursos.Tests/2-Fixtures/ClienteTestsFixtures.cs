using Recursos.Clientes;
using System;
using Xunit;

namespace Recursos.Tests._2_Fixtures
{

    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteTestsFixture> { }

    public class ClienteTestsFixture : IDisposable
    {
        public Cliente GerarClienteValido() => 
            new Cliente(
                Guid.NewGuid(),
                "André",
                "Dantas",
                DateTime.Now.AddYears(-35),
                DateTime.Now,
                "dede@dede.com",
                true);

        public Cliente GerarClienteInvalido() =>
            new Cliente(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                DateTime.Now,
                "dede@dede.com",
                true);

        public void Dispose()
        {            
        }
    }
}
