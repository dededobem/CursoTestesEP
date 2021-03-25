using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Demo.Tests
{
    public class AssertStringsTests
    {
        [Fact]
        public void StringsTools_UnirNomes_RetornarNomeCompleto()
        {
            //Arrange
            var unirStr = new StringsTools();

            //Act
            var nomeCompleto = unirStr.Unir("André", "Dantas");

            //Assert
            Assert.Equal("André Dantas", nomeCompleto);
        }

        [Fact]
        public void StringsTools_UnirNomes_IgnoreCase()
        {
            //Arrange 
            var unirStr = new StringsTools();

            //Act
            var nomeCompleto = unirStr.Unir("André", "Dantas");

            //Assert
            Assert.Equal("ANDRÉ DANTAS", nomeCompleto, true);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveConterTrecho()
        {
            //Arrange
            var unirStr = new StringsTools();

            //Act
            var nomeCompleto = unirStr.Unir("André", "Dantas");

            //Assert
            Assert.Contains("ndr", nomeCompleto);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveComecarCom()
        {
            //Arrange
            var unirStr = new StringsTools();

            //Act
            var nomeCompleto = unirStr.Unir("André", "Dantas");

            //Assert
            Assert.StartsWith("And", nomeCompleto);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveAcabarCom()
        {
            //Arrange
            var unirStr = new StringsTools();

            //Act
            var nomeCompleto = unirStr.Unir("André", "Dantas");

            //Assert
            Assert.EndsWith("tas", nomeCompleto);
        }

        [Fact]
        public void StringsTools_UnirNomes_ValidarExpressaoRegular()
        {
            //Arrange
            var unirStr = new StringsTools();

            //Act
            var nomeCompleto = unirStr.Unir("André", "Dantas");

            //Assert
            Assert.Matches("[A-Z]{1}[a-z]+", nomeCompleto);
        }
    }
}
