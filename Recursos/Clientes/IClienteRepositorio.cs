using Recursos.Core;

namespace Recursos.Clientes
{
    public interface IClienteRepositorio : IRepositorio<Cliente>
    {
        Cliente ObterPorEmail(string email);
    }
}
