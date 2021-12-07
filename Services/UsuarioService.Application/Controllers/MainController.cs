using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using UsuarioService.Application.ViewModels;
using UsuarioService.Business.Interfaces;
using UsuarioService.Business.Notificacoes;

namespace UsuarioService.Application.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador _notificador;

        protected Guid UsuarioId { get; set; }

        protected MainController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected IActionResult CustomResponse(object token = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    JWT = token
                });
            }

            return BadRequestComErros();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMessage = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMessage);
            }
        }

        protected IActionResult BadRequestComErros()
        {
            return BadRequest(new
            {
                errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }

        protected ActionResult ReturnNotFound()
        {
            return NotFound(new
            {
                errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected IdentityUser IdentityUserFactory(RegisterUserVm registerUserViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerUserViewModel.Email,
                Email = registerUserViewModel.Email,
                EmailConfirmed = true
            };

            return identityUser;
        }

        protected bool ValidarModelState(ModelStateDictionary modelState)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida(ModelState);
                return false;
            }

            return true;
        }
    }
}
