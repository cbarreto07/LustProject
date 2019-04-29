using System;
using System.ComponentModel.DataAnnotations;

namespace Lust.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]        
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Required]
        [Display(Name = "Cpf")]
        public string Cpf { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }


        [Required]
        [Display(Name = "Cep")]
        public string Cep { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
