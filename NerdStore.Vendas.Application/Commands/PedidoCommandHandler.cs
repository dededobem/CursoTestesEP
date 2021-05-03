using MediatR;
using NerdStore.Vendas.Application.Events;
using NerdStore.Vendas.Domain;
using NerdStore.Vendas.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Commands
{
    public class PedidoCommandHandler : IRequestHandler<AdicionarItemPedidoCommand, bool>
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IMediator _mediator;

        public PedidoCommandHandler(IPedidoRepositorio pedidoRepositorio, IMediator mediator)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _mediator = mediator;
        }
        
        public async Task<bool> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepositorio.ObterPedidoRascunhoPorClienteId(message.ClienteId);
            var pedidoItem = new PedidoItem(message.ProdutoId, message.Nome, message.Quantidade, message.ValorUnitario);

            if(pedido == null)
            {
                pedido = Pedido.PedidoFactory.NovoPedidoRascunho(message.ClienteId);
                pedido.AdicionarItem(pedidoItem);

                _pedidoRepositorio.Adicionar(pedido);
            }
            else
            {
                pedido.AdicionarItem(pedidoItem);
                _pedidoRepositorio.AdicionarItem(pedidoItem);
                _pedidoRepositorio.Atualizar(pedido);
            }

            
            
            
            _pedidoRepositorio.Adicionar(pedido);

            pedido.AdicionarEvento(new PedidoItemAdicionadoEvent(pedido.ClienteId, pedido.Id, message.ProdutoId, 
                message.Nome, message.Quantidade, message.ValorUnitario));

            return await _pedidoRepositorio.UnitOfWork.Commit();
        }
    }
}
