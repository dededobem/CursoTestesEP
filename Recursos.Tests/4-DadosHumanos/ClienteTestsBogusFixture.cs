using Bogus;
using Bogus.DataSets;
using Recursos.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Recursos.Tests._4_DadosHumanos
{

    [CollectionDefinition(nameof(ClienteBogusCollection))]
    public class ClienteBogusCollection : ICollectionFixture<ClienteTestsBogusFixture> { }

    public class ClienteTestsBogusFixture : IDisposable
    {
        public Cliente GerarClienteValido() => GerarClientes(1, true).FirstOrDefault();

        public IEnumerable<Cliente> GerarClientesVariados()
        {
            var clientes = new List<Cliente>();

            clientes.AddRange(GerarClientes(50, true).ToList());
            clientes.AddRange(GerarClientes(50, false).ToList());

            return clientes;
        }

        public IEnumerable<Cliente> GerarClientes(int quantidade, bool ativo)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var clientes = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f => new Cliente(
                    Guid.NewGuid(),
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    f.Date.Past(80, DateTime.Now.AddYears(-18)),
                    DateTime.Now,
                    "",
                    ativo))
                .RuleFor(c => c.Email, (f, c) =>
                    f.Internet.Email(c.Nome.ToLower(), c.Sobrenome.ToLower()));

            return clientes.Generate(10);
        }

        public Cliente GerarClienteInvalido() {
            var genero = new Faker().PickRandom<Name.Gender>();

            var cliente = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f => new Cliente(
                    Guid.NewGuid(),
                    f.Name.FirstName(genero),
                    f.Name.LastName(genero),
                    f.Date.Past(1, DateTime.Now.AddYears(1)),
                    DateTime.Now,
                    "",
                    false));

            return cliente;
        }

        public void Dispose()
        {
        }
    }
}

