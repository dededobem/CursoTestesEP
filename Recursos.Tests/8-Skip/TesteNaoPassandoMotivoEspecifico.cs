using Xunit;

namespace Recursos.Tests._8_Skip
{    
    public class TesteNaoPassandoMotivoEspecifico
    {
        [Fact(DisplayName = "Novo cliente 2.0", Skip = "Quebrando na nova versão")]
        [Trait("Categoria", "Skip teste")]
        public void Teste_NaoEstaPassando_VersaoNovaNaoCompativel()
        {
            Assert.True(false);
        }

    }
}
