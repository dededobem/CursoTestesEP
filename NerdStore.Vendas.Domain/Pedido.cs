using FluentValidation.Results;
using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Vendas.Domain
{
    public class Pedido
    {
        public static int MAX_UNIDADES_ITEM => 15;
        public static int MIN_UNIDADES_ITEM => 1;
        protected Pedido()
        {
            _pedidoItems = new List<PedidoItem>();
        }

        public readonly List<PedidoItem> _pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;
        public decimal ValorTotal { get; private set; }
        public PedidoStatus Status { get; private set; }
        public Guid ClienteId { get; private set; }
        public Voucher Voucher { get; private set; }
        public bool VoucherUtilizado { get; private set; }
        public decimal Desconto { get; private set; }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            ValidarQuantidadePermitidaItems(pedidoItem);
            
            if (PedidoItemExistente(pedidoItem))
            {               
                var itemExistente = _pedidoItems.FirstOrDefault(p => p.Id == pedidoItem.Id);
                itemExistente.AdicionarUnidades(pedidoItem.Quantidade);
                pedidoItem = itemExistente;

                _pedidoItems.Remove(itemExistente);
            }

            _pedidoItems.Add(pedidoItem);
            CalcularValorPedido();
        }

        public void AtualizarItem(PedidoItem item)
        {
            ValidarPedidoItemInexistente(item);
            ValidarQuantidadePermitidaItems(item);

            var itemExistente = _pedidoItems.FirstOrDefault(p => p.Id == item.Id);
            _pedidoItems.Remove(itemExistente);

            _pedidoItems.Add(item);

            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem item)
        {
            ValidarPedidoItemInexistente(item);
                        
            _pedidoItems.Remove(item);

            CalcularValorPedido();
        }

        private void ValidarPedidoItemInexistente(PedidoItem item)
        {
            if (!PedidoItemExistente(item)) throw new DomainException($"Item não encontrado");
        }

        private void ValidarQuantidadePermitidaItems(PedidoItem item)
        {
            var quantidade = item.Quantidade;
            if (PedidoItemExistente(item))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(p => p.Id == item.Id);
                quantidade += itemExistente.Quantidade;
            }

            if(quantidade > MAX_UNIDADES_ITEM) throw new DomainException($"Máximo de {MAX_UNIDADES_ITEM} unidades por item");
        }

        private bool PedidoItemExistente(PedidoItem item) => _pedidoItems.Any(p => p.Id == item.Id);

        private void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(i => i.CalcularValor());
        }

        public void TornarPedidoRascunho()
        {
            Status = PedidoStatus.Rascunho;
        }

        public ValidationResult AplicarVoucher(Voucher voucher) 
        {            
            var result = voucher.ValidarSeAplicavel();
            if (!result.IsValid) return result;

            Voucher = voucher;
            VoucherUtilizado = true;

            CalcularValorTotalDesconto();

            return result;
        }

        public void CalcularValorTotalDesconto()
        {
            if (!VoucherUtilizado) return;

            decimal desconto = 0;

            if(Voucher.TipoDescontoVoucher == TipoDescontoVoucher.Valor)
            {
                if (Voucher.ValorDesconto.HasValue)
                {
                    desconto = Voucher.ValorDesconto.Value;
                }
            }
            else
            {
                if (Voucher.PercentualDesconto.HasValue)
                {
                    desconto = ValorTotal * (Voucher.PercentualDesconto.Value / 100);
                }
            }

            ValorTotal -= desconto;
            Desconto = desconto;
        }
        

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido()
                {
                    ClienteId = clienteId
                };
                pedido.TornarPedidoRascunho();
                return pedido;
            }
        }

    }

    public enum PedidoStatus
    {
        Rascunho = 0,
        Iniciado = 1,
        Pago = 2,
        Entregue = 3,
        Cancelado = 6
    }

}
