using FluentValidation;
using Recursos.Core;
using System;

namespace Recursos.Clientes
{
    public class Cliente : Entidade
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Email { get; private set; }
        public bool Ativo { get; private set; }

        protected Cliente()
        {

        }

        public Cliente(Guid id, string nome, string sobrenome, DateTime dataNascimento, DateTime dataCadastro, string email, bool ativo)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;
            DataCadastro = dataCadastro;
            Email = email;
            Ativo = ativo;
        }

        public string NomeCompleto(string nome, string sobrenome) => $"{nome} {sobrenome}";

        public bool EhEspecial() => DataCadastro < DateTime.Now.AddYears(-3) && Ativo;

        public void Inativar() => Ativo = false;

        public override bool EhValido()
        {
            ValidationResult = new ClienteValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ClienteValidacao : AbstractValidator<Cliente>
    {
        public ClienteValidacao()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Nome obrigatório!")
                .Length(2, 150).WithMessage("Nome deve possuir entre 2 e 150 caracteres");

            RuleFor(c => c.Sobrenome)
                .NotEmpty().WithMessage("Sobrenome obrigatório!")
                .Length(2, 150).WithMessage("Sobrenome deve possuir entre 2 e 150 caracteres");

            RuleFor(c => c.DataNascimento)
                .NotEmpty()
                .Must(HaveMinimunAge)
                .WithMessage("O cliente deve ser maior de 18 anos!");

            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        public static bool HaveMinimunAge(DateTime dataAniversario) => 
            dataAniversario <= DateTime.Now.AddYears(-18);

    }
}
