using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recursos.Clientes
{
    public class ClienteEmailNotificacao : INotification
    {
        public ClienteEmailNotificacao(string origem, string destino, string assunto, string mensagem)
        {
            Origem = origem;
            Destino = destino;
            Assunto = assunto;
            Mensagem = mensagem;
        }

        public string Origem { get; private set; }
        public string Destino { get; private set; }
        public string Assunto { get; private set; }
        public string Mensagem { get; private set; }
    }
}
