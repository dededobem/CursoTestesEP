using NerdStore.Core.Data;
using System;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Domain.Interfaces
{
    public interface IPedidoRepositorio : IRepository<Pedido>
    {
        void Adicionar(Pedido pedido);
        void AdicionarItem(PedidoItem pedidoItem);
        void Atualizar(Pedido pedido);
        Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId);
    }
}
