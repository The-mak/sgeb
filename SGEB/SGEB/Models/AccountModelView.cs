using System.ComponentModel.DataAnnotations;

namespace SGEB.Models
{
    public class LoginModelView
    {
        [Required]
        [Display(Name = "Nome do usuário")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Mantenha-me logado")]
        public bool KeepLogged { get; set; }
    }
}