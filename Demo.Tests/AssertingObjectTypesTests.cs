using Xunit;

namespace Demo.Tests
{
    public class AssertingObjectTypesTests
    {
        [Fact]
        public void FuncionarioFactory_Criar_DeveRetornarTipoFuncionario()
        {
            //Arrange e Act
            var funcionario = FuncionarioFactory.Criar("André", 9000);

            //Assert
            Assert.IsType<Funcionario>(funcionario);
        }

        [Fact]
        public void FuncionarioFactory_Criar_DeveRetornarTipoDerivadoPessoa()
        {
            //Arrange e Act
            var funcionario = FuncionarioFactory.Criar("André", 9000);

            //Assert
            Assert.IsAssignableFrom<Pessoa>(funcionario);
        }
    }
}
