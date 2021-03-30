using Xunit;

namespace Demo.Tests
{
    public class AssertingCollectionsTests
    {
        [Fact]
        public void Funcionario_Habilidades_NaoDevePossuirHabilidadesVazias()
        {
            //Arrange e Act
            var funcionario = FuncionarioFactory.Criar("André", 10000);

            //Assert
            Assert.All(funcionario.Habilidades, habilidades => Assert.False(string.IsNullOrWhiteSpace(habilidades)));
        }

        [Fact]
        public void Funcionario_Habilidades_JuniorDevePossuirHabilidadesBasicas()
        {
            //Arrange e Act
            var funcionario = new Funcionario("André", 1000);

            //Assert
            Assert.Contains("OOP", funcionario.Habilidades);
            Assert.Contains("Lógica de Programação", funcionario.Habilidades);

        }

        [Fact]
        public void Funcionario_Habilidades_JuniorNaoDevePossuirHabilidadesAvancadas()
        {
            //Arrange e Act
            var funcionario = new Funcionario("André", 1000);

            //Assert
            Assert.DoesNotContain("Testes", funcionario.Habilidades);
            Assert.DoesNotContain("Microserviços", funcionario.Habilidades);

        }

        [Fact]
        public void Funcionario_Habilidades_SeniorDevePossuirTodasHabilidades()
        {
            //Arrange e Act
            var funcionario = new Funcionario("André", 10000);

            var habilidadesSenior = new []
            {
                "Lógica de Programação",
                "OOP",
                "Testes",
                "Microserviços"
            };

            //Assert
            Assert.Equal(habilidadesSenior, funcionario.Habilidades);

        }
    }
}
