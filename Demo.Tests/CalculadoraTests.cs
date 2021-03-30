using Xunit;

namespace Demo.Tests
{
    public class CalculadoraTests
    {
        [Fact]
        public void Calculadora_Somar_RetornarValorSoma()
        {
            //Arrange
            var calculadora = new Calculadora();

            //Act
            var resultado = calculadora.Somar(2, 2);

            //Assert
            Assert.Equal(4, resultado);

        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(3, 3, 6)]
        [InlineData(2, 5, 7)]
        [InlineData(3, 8, 11)]
        [InlineData(4, 1, 5)]
        public void Calculadora_Somar_RetornarValoresDiversosDaSoma(double v1, double v2, double resultado)
        {
            //Arrange
            var calculadora = new Calculadora();

            //Act
            var result = calculadora.Somar(v1, v2);

            //Assert
            Assert.Equal(result, resultado);
        }
    }
}
