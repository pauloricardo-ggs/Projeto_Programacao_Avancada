using System.ComponentModel.DataAnnotations;

namespace UsuarioService.Application.ViewModels
{
    public class UserVm
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }
    }
}