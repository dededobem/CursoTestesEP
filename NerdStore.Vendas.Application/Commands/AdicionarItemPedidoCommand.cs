using FluentValidation;
using NerdStore.Core.Messages;
using NerdStore.Vendas.Domain;
using System;

namespace NerdStore.Vendas.Application.Commands
{
    public class AdicionarItemPedidoCommand : Command
    {
        public AdicionarItemPedidoCommand(Guid clienteId, Guid produtoId, string nome, int quantidade, decimal valorUnitario)
        {
            ClienteId = clienteId;
            ProdutoId = produtoId;
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }


        public Guid ClienteId { get; set; }
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarItemPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarItemPedidoValidation : AbstractValidator<AdicionarItemPedidoCommand>
    {
        public static string IdClienteErrorMsg => "Id do cliente inválido";
        public static string IdProdutoErrorMsg => "Id do produto inválido";
        public static string NomeErrorMsg => "O nome do produto não foi informado";
        public static string QtdMaxErrorMsg => $"A quantidade máxima de um item é {Pedido.MAX_UNIDADES_ITEM}";
        public static string QtdMinErrorMsg => $"A quantidade mínima de um item é 1";
        public static string ValorErrorMsg => "O valor do item precisa ser maior que 0";

        public AdicionarItemPedidoValidation()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdClienteErrorMsg);

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdProdutoErrorMsg);

            RuleFor(c => c.ClienteId)
                .NotEmpty()
                .WithMessage(NomeErrorMsg);

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage(QtdMinErrorMsg)
                .LessThanOrEqualTo(Pedido.MAX_UNIDADES_ITEM)
                .WithMessage(QtdMaxErrorMsg);

            RuleFor(c => c.ValorUnitario)
                 .GreaterThan(0)
                 .WithMessage(ValorErrorMsg);
        }
    }
}


