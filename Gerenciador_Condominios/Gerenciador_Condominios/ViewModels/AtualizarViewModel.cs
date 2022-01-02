using System.ComponentModel.DataAnnotations;

namespace Gerenciador_Condominios.ViewModels
{
    public class AtualizarViewModel
    {
        public string UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        [Display(Name = "Usuário")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Telefone { get; set; }

        public string? Foto { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
    }
}
