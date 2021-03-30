using Xunit;

namespace Demo.Tests
{
    public class AssertNumbersTests
    {
        [Fact]
        public void Calculadora_Somar_DeveSerIgual()
        {
            //Arrange
            var caluladora = new Calculadora();

            //Act
            var soma = caluladora.Somar(1, 3);

            //Assert
            Assert.Equal(4, soma);
        } 

        [Fact]
        public void Calculadora_Somar_NaoDeverSerIgual()
        {
            //Arrange
            var caluladora = new Calculadora();

            //Act
            var soma = caluladora.Somar(1.131233123, 2.231233123);

            //Assert
            Assert.NotEqual(3.3, soma, 1);
        }
    }
}
