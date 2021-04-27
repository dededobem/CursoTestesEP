using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Recursos.Tests._3_Order
{
    [TestCaseOrderer("Recursos.Tests._3_Order.PriorityOrderer", "Recursos.Tests._3_Order")]
    public class OrdemTestes
    {
        public static bool Teste1Chamado;
        public static bool Teste2Chamado;
        public static bool Teste3Chamado;
        public static bool Teste4Chamado;

        [Fact(DisplayName = "Teste 04", Skip = "Rever teste de ordenação"), TestPriority(3)]
        [Trait("Categoria", "Ordenação Testes")]
        public void Teste04()
        {
            Teste4Chamado = true;

            Assert.True(Teste3Chamado);
            Assert.True(Teste1Chamado);
            Assert.False(Teste2Chamado);

        }

        [Fact(DisplayName = "Teste 01", Skip = "Rever teste de ordenação"), TestPriority(2)]
        [Trait("Categoria", "Ordenação Testes")]
        public void Teste01()
        {
            Teste1Chamado = true;

            Assert.True(Teste3Chamado);
            Assert.False(Teste4Chamado);
            Assert.False(Teste2Chamado);

        }

        [Fact(DisplayName = "Teste 03", Skip = "Rever teste de ordenação"), TestPriority(1)]
        [Trait("Categoria", "Ordenação Testes")]
        public void Teste03()
        {
            Teste3Chamado = true;

            Assert.False(Teste1Chamado);
            Assert.False(Teste2Chamado);
            Assert.False(Teste4Chamado);

        }

        [Fact(DisplayName = "Teste 02", Skip = "Rever teste de ordenação"), TestPriority(4)]
        [Trait("Categoria", "Ordenação Testes")]
        public void Teste02()
        {
            Teste2Chamado = true;

            Assert.True(Teste3Chamado);
            Assert.True(Teste4Chamado);
            Assert.True(Teste1Chamado);

        }
    }
}
