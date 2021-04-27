using FluentAssertions;
using Recursos.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Recursos.Tests._9_CodeCoverage
{
    public class CpfValidationTests
    {
        [Theory(DisplayName = "Cpf válidos")]
        [Trait("Categoria", "CPF Validation Tests")]
        [InlineData("01786171562")]
        [InlineData("05589797500")]
        [InlineData("02503508588")]
        public void Cpf_ValidarMultiplosNumeros_TodosDevemSerValidos(string cpf)
        {
            //Arrange
            var cpfValidation = new CpfValidation();

            //Act & Assert
            cpfValidation.EhValido(cpf).Should().BeTrue();
        }

        [Theory(DisplayName = "Cpf inválidos")]
        [Trait("Categoria", "CPF Validation Tests")]
        [InlineData("0176586171562")]
        [InlineData("05583397500")]
        [InlineData("02522508588")]
        [InlineData("05589566700")]
        public void Cpf_ValidarMultiplosNumeros_TodosDevemSerInValidos(string cpf)
        {
            //Arrange
            var cpfValidation = new CpfValidation();

            //Act & Assert
            cpfValidation.EhValido(cpf).Should().BeFalse();
        }
    }
}
