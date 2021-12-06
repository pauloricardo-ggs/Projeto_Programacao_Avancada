using System;

namespace UsuarioService.Business.Models
{
    public class Usuario
    {
        public string Id { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }

        public Usuario(string email, string senha)
        {
            Id = Guid.NewGuid().ToString();
            Email = email;
            Senha = senha;
        }
    }
}
