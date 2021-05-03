using NerdStore.Core.DomainObjects;
using System;
using System.Linq;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoTests
    {
        [Fact(DisplayName = "Adicionar Item Novo Pedido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_NovoPedido_DeveAtualizarValor()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var itemPedido = new PedidoItem(Guid.NewGuid(), "Nome do item", 2, 100);

            //Act
            pedido.AdicionarItem(itemPedido);

            //Assert
            Assert.Equal(200, pedido.ValorTotal);

        }

        [Fact(DisplayName = "Adicionar Item Pedido Existente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemExistente_DeveIncrementarUnidadesSomarValores()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoId = Guid.NewGuid();
            var itemPedido1 = new PedidoItem(pedidoId, "Mochila", 1, 100);
            pedido.AdicionarItem(itemPedido1);

            var itemPedido2 = new PedidoItem(pedidoId, "Mochila", 2, 100);

            //Act
            pedido.AdicionarItem(itemPedido2);

            //Assert
            Assert.Equal(300, pedido.ValorTotal);
            Assert.Equal(1, pedido.PedidoItems.Count);
            Assert.Equal(3, pedido.PedidoItems.FirstOrDefault(p => p.Id == pedidoId).Quantidade);

        }

        [Fact(DisplayName = "Adicionar Item pedido acima do permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_UnidadesItemAcimaDoPermitido_DeveRetornarException()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoId = Guid.NewGuid();
            var itemPedido = new PedidoItem(pedidoId, "Mochila", Pedido.MAX_UNIDADES_ITEM + 1, 100);
                      
            //Act & Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(itemPedido));
        }

        [Fact(DisplayName = "Adicionar Item pedido existente acima do permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemExistenteSomaUnidadesAcimaDoPermitido_DeveRetornarException()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoId = Guid.NewGuid();
            var itemPedido1 = new PedidoItem(pedidoId, "Mochila", 1, 100);
            var itemPedido2 = new PedidoItem(pedidoId, "Mochila", Pedido.MAX_UNIDADES_ITEM + 1, 100);
            pedido.AdicionarItem(itemPedido1);

            //Act & Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(itemPedido2));
        }
    
        [Fact(DisplayName = "Atualizar Item Pedido Inexistente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemNaoExisteNaLista_DeveRetornaException()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoId = Guid.NewGuid();
            var itemPedidoAtualizar = new PedidoItem(pedidoId, "Mochila", 5, 100);

            //Act & Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(itemPedidoAtualizar));
        }

        [Fact(DisplayName = "Atualizar Item Pedido Válido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemValido_DeveAtualizarQuantidade()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var itemId = Guid.NewGuid();
            var itemPedido = new PedidoItem(itemId, "Mochila", 2, 100);
            pedido.AdicionarItem(itemPedido);
            var itemPedidoAtualizar = new PedidoItem(itemId, "Mochila", 5, 100);
            var novaQuantidade = itemPedidoAtualizar.Quantidade;

            //Act
            pedido.AtualizarItem(itemPedidoAtualizar);

            //Assert
            Assert.Equal(novaQuantidade, pedido.PedidoItems.FirstOrDefault(p => p.Id == itemId).Quantidade);
            
        }

        [Fact(DisplayName = "Atualizar Item Pedido Validar Total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_PedidoComValorDiferente_DeveAtualizarValorTotal()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var itemId = Guid.NewGuid();
            var itemPedido1 = new PedidoItem(Guid.NewGuid(), "Caderno", 2, 20);
            var itemPedido2 = new PedidoItem(itemId, "Mochila", 3, 50);
            pedido.AdicionarItem(itemPedido1);
            pedido.AdicionarItem(itemPedido2);
            var itemPedidoAtualizar = new PedidoItem(itemId, "Mochila", 3, 100);
            var novoValorTotal = itemPedidoAtualizar.ValorUnitario * itemPedidoAtualizar.Quantidade +
                                    itemPedido1.ValorUnitario * itemPedido1.Quantidade;

            //Act
            pedido.AtualizarItem(itemPedidoAtualizar);

            //Assert
            Assert.Equal(novoValorTotal, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Atualizar Item Pedido Quantidade Acima do Permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemUnidadesAcimaDoPermitido_DeveRetornarException()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoId = Guid.NewGuid();
            var itemPedido = new PedidoItem(pedidoId, "Mochila", 3, 15);            
            pedido.AdicionarItem(itemPedido);

            var itemPedidoAtualizar = new PedidoItem(pedidoId, "Mochila", Pedido.MAX_UNIDADES_ITEM + 1, 15);

            //Act & Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(itemPedidoAtualizar));
        }

        [Fact(DisplayName = "Remover Item Pedido Inexistente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void RemoverItemPedido_ItemNaoExisteNaLista_DeveRetornaException()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoId = Guid.NewGuid();
            var itemPedidoRemover = new PedidoItem(pedidoId, "Mochila", 5, 100);

            //Act & Assert
            Assert.Throws<DomainException>(() => pedido.RemoverItem(itemPedidoRemover));
        }

        [Fact(DisplayName = "Remover Item Pedido Deve Atualizar Valor Total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void RemoverItemPedido_ItemExistente_DeveAtualizarQuantidade()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var itemId = Guid.NewGuid();
            var itemPedido1 = new PedidoItem(itemId, "Caderno", 2, 100);
            var itemPedido2 = new PedidoItem(Guid.NewGuid(), "Mochila", 3, 15);
            pedido.AdicionarItem(itemPedido1);
            pedido.AdicionarItem(itemPedido2);

            var totalPedido = itemPedido2.Quantidade * itemPedido2.ValorUnitario;

            //Act
            pedido.RemoverItem(itemPedido1);

            //Assert
            Assert.Equal(totalPedido, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar Voucher Válido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void Pedido_AplicarVoucherValido_DeveRetornarSemErro()
        {
            //Arrange
            var voucher = new Voucher("PROMO-15-REAIS", 15, null, 1, DateTime.Now.AddDays(15),
                true, false, TipoDescontoVoucher.Valor);

            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            //Act
            var result = pedido.AplicarVoucher(voucher);

            //Assert
            Assert.True(result.IsValid);

        }

        [Fact(DisplayName = "Aplicar Voucher Inválido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void Pedido_AplicarVoucherInvalido_DeveRetornarSemErro()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var voucher = new Voucher("", null, null, 0, DateTime.Now.AddDays(-1),
                false, true, TipoDescontoVoucher.Valor);            

            //Act
            var result = pedido.AplicarVoucher(voucher);

            //Assert
            Assert.False(result.IsValid);

        }

        [Fact(DisplayName = "Aplicar Voucher Tipo Valor Desconto")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AplicarVoucher_AplicarVoucherTipoValor_DeveDescontarDoValorTotal()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var voucher = new Voucher("PROMO-200-REAIS", 200, null, 1, DateTime.Now.AddDays(15),
                true, false, TipoDescontoVoucher.Valor);
            var itemId = Guid.NewGuid();
            var itemPedido = new PedidoItem(itemId, "Mesa", 1, 1000);
            pedido.AdicionarItem(itemPedido);
            var valorDesconto = pedido.ValorTotal - voucher.ValorDesconto;

            //Act
            var result = pedido.AplicarVoucher(voucher);

            //Assert
            Assert.Equal(valorDesconto, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar Voucher Tipo Percentual Desconto")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AplicarVoucher_AplicarVoucherTipoPercentual_DeveDescontarDoValorTotal()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var voucher = new Voucher("PROMO-10-PORCENTO", null, 10, 1, DateTime.Now.AddDays(15),
                true, false, TipoDescontoVoucher.Porcentagem);
            var itemId = Guid.NewGuid();
            var itemPedido = new PedidoItem(itemId, "Mesa", 1, 1000);
            pedido.AdicionarItem(itemPedido);
            var valorDesconto = pedido.ValorTotal - (pedido.ValorTotal * (voucher.PercentualDesconto / 100));

            //Act
            var result = pedido.AplicarVoucher(voucher);

            //Assert
            Assert.Equal(valorDesconto, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar Voucher Desconto Excede Valor Total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AplicarVoucher_DescontoExcedeValorTotalPedido_PedidoDeveTerValorZerado()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var itemPedido = new PedidoItem(Guid.NewGuid(), "Xbox", 1, 2000);
            var voucher = new Voucher("PROMO-3000-REAIS", 3000, null, 1, DateTime.Now.AddDays(15),
                true, false, TipoDescontoVoucher.Valor);

            //Act
            pedido.AplicarVoucher(voucher);

            //Assert
            Assert.Equal(0, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar Voucher Re-caucular Desconto na Modificação do Pedido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AplicarVoucher_ModificarItensPedido_DeveCalcularDescontoValorTotal()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var itemPedido = new PedidoItem(Guid.NewGuid(), "Xbox", 1, 2000);
            pedido.AdicionarItem(itemPedido);

            var voucher = new Voucher("PROMO-1000-REAIS", 1000, null, 1, DateTime.Now.AddDays(15),
                true, false, TipoDescontoVoucher.Valor);
            pedido.AplicarVoucher(voucher);

            var itemPedido2 = new PedidoItem(Guid.NewGuid(), "Smart Tv", 2, 4000);

            //Act
            pedido.AdicionarItem(itemPedido2);

            //Assert
            var totalEsperado = pedido.PedidoItems.Sum(i => i.Quantidade * i.ValorUnitario) - voucher.ValorDesconto;
            Assert.Equal(totalEsperado, pedido.ValorTotal);
        }


    }
}
