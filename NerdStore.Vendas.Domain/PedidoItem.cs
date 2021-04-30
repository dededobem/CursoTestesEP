using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Vendas.Domain
{
    public class PedidoItem
    {
        public PedidoItem(Guid id, string nome, int quantidade, decimal valorUnitario)
        {
            if (quantidade < Pedido.MIN_UNIDADES_ITEM) 
                throw new DomainException($"Mínimo de {Pedido.MIN_UNIDADES_ITEM} unidades por item");

            Id = id;
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        internal void AdicionarUnidades(int unidades) => Quantidade += unidades;

        internal decimal CalcularValor() => Quantidade * ValorUnitario;
    }
}
