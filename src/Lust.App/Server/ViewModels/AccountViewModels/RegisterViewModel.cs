using System;
using System.ComponentModel.DataAnnotations;

namespace Lust.App.Server.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(250)]
        [Display(Name = "Nome/apelido")]
        public string Nome { get; set; }
                
        [Required]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]        
        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Required]
        [StringLength(11)]        
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required]
        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [StringLength(10)]        
        [Display(Name = "Cep")]
        public string Cep { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0}no mínimo {2} e no máximo {1} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

    }
}
