using System.Collections.Generic;
using UsuarioService.Business.Notificacoes;

namespace UsuarioService.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}