using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsuarioService.Application.ViewModels;
using UsuarioService.Business.Interfaces;

namespace UsuarioService.Application.Controllers
{
    [ApiController]
    public class UsuarioController : MainController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UsuarioController(INotificador notificador,
                              UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager) : base(notificador)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("usuario/cadastrar")]
        public async Task<IActionResult> Cadastrar(RegisterUserVm registerUserVm)
        {
            if (!ValidarModelState(ModelState)) { return BadRequestComErros(); }

            var usuario = IdentityUserFactory(registerUserVm);
            var result = await _userManager.CreateAsync(usuario, registerUserVm.Senha);

            if (result.Succeeded)
            {
                return CreatedAtAction("Cadastrar", null);
            }

            foreach (var error in result.Errors)
            {
                NotificarErro(error.Description);
            }
            return BadRequestComErros();
        }

        [HttpPost("usuario/entrar")]
        public async Task<IActionResult> Entrar(LoginUserVm loginUserVm)
        {
            if (!ValidarModelState(ModelState)) { return BadRequestComErros(); }

            var result = await _signInManager.PasswordSignInAsync(loginUserVm.Email, loginUserVm.Senha, false, true);

            if (result.Succeeded)
            {
                return Ok();
            }
            if (result.IsLockedOut)
            {
                NotificarErro("Usuário temporariamente bloqueado por tentativas inválidas");
                return BadRequestComErros();
            }

            NotificarErro("Usuário ou Senha incorretos");
            return BadRequestComErros();
        }

        [HttpDelete("usuario/excluir/{id}")]
        public async Task<IActionResult> Excluir(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                NotificarErro("Nenhum usuário foi encontrado com o id informado");
                return BadRequestComErros();
            }
            var result = await _userManager.DeleteAsync(user);

            if(result.Succeeded) return NoContent();
            return BadRequest();
        }

        [HttpPut("usuario/atualizar/{id}")]
        public async Task<IActionResult> Atualizar(string id, UserVm userVm)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                NotificarErro("Nenhum usuário foi encontrado com o id informado");
                return BadRequestComErros();
            }

            user.Email = userVm.Email;
            user.NormalizedEmail = userVm.Email.ToUpper();
            user.UserName = userVm.Email;
            user.NormalizedUserName = userVm.Email.ToUpper();

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return NoContent();
            return BadRequest();

        }

        [HttpGet("usuarios")]
        public async Task<IList<IdentityUser>> ObterTodos()
        {
            var usuarios = await _userManager.Users.ToListAsync();
            return usuarios;
        }

        [HttpGet("usuario/{id}")]
        public async Task<IdentityUser> ObterPorId(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            return usuario;
        }
    }
}
