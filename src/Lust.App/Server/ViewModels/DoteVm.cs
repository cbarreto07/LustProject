using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.ViewModels
{
    public class DoteVM
    {
        public DoteVM()
        {
            Id = Guid.NewGuid();
        }

        public Guid? Id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MinLength(2, ErrorMessage = "O tamanho minimo é {1}")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo  é {1}")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}
