using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recursos.Clientes
{
    public class ClienteServico : IClienteServico
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IMediator _mediator;

        public ClienteServico(IClienteRepositorio clienteRepositorio, IMediator mediator)
        {
            _clienteRepositorio = clienteRepositorio;
            _mediator = mediator;
        }

        public void Adicionar(Cliente cliente)
        {
            if (!cliente.EhValido())
                return;
            
            _clienteRepositorio.Adicionar(cliente);
            _mediator.Publish(new ClienteEmailNotificacao("dede@dede.com", cliente.Email, "Assunto", "Mensagem"));
        }

        public void Atualizar(Cliente cliente)
        {
            if (!cliente.EhValido())
                return;

            _clienteRepositorio.Atualizar(cliente);
            _mediator.Publish(new ClienteEmailNotificacao("dede@dede.com", cliente.Email, "Assunto", "Mensagem"));
        }

        public void Dispose()
        {
            _clienteRepositorio.Dispose();
        }

        public void Inativar(Cliente cliente)
        {
            if (!cliente.EhValido())
                return;

            cliente.Inativar();
            _clienteRepositorio.Atualizar(cliente);
            _mediator.Publish(new ClienteEmailNotificacao("dede@dede.com", cliente.Email, "Assunto", "Mensagem"));
        }

        public IEnumerable<Cliente> ObterTodosAtivos() =>
            _clienteRepositorio.ObterTodosAtivos().Where(c => c.Ativo);
        

        public void Remover(Cliente cliente)
        {
            _clienteRepositorio.Remover(cliente);
            _mediator.Publish(new ClienteEmailNotificacao("dede@dede.com", cliente.Email, "Assunto", "Mensagem"));
        }
    }
}
