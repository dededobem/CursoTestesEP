using Recursos.Clientes;
using System;
using System.Collections.Generic;

namespace Recursos.Core
{
    public interface IRepositorio<T> where T : class
    {
        IEnumerable<Cliente> ObterTodosAtivos();
        void Adicionar(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Remover(Cliente cliente);
        void Dispose();
    }
}
