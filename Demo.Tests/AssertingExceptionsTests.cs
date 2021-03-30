using System;
using Xunit;

namespace Demo.Tests
{
    public class AssertingExceptionsTests
    {
        [Fact]
        public void Calculadora_Dividir_DeveRetornarErroDivisaoPorZero()
        {
            //Arrange
            var calculadora = new Calculadora();

            //Act e Assert
            Assert.Throws<DivideByZeroException>(() => calculadora.Dividir(10, 0));
        }

        [Fact]
        public void Funcionario_Salario_DeveRetornarErroSalarioInferiorPermitido()
        {
            //Arrange e Act e Assert
            var exception = Assert.Throws<Exception>(() => FuncionarioFactory.Criar("André", 400));
                        
            Assert.Equal("Salário inferior ao permitido!", exception.Message);
        }

    }
}
