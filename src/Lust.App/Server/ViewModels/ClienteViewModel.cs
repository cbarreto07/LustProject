using Lust.Domain.Clientes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.ViewModels
{
    public class ClienteViewModel
    {
        public ClienteViewModel()
        {
            Id = Guid.NewGuid();
        
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Nome é requerido")]
        [MinLength(2, ErrorMessage = "O tamanho minimo do Nome é {1}")]
        [MaxLength(100, ErrorMessage = "O tamanho máximo do Nome é {1}")]
        [Display(Name = "Nome do Evento")]
        public string Nome { get; set; }

        public string Email { get; set; }

        public EnumGenero Genero { get; set; }

        public string Celular { get; set; }

        public string Cpf { get; set; }

        public DateTime DataNascimento { get; set; }

        public bool EstaOferecendo { get; set; }

        public bool EstaDesfrutando { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }



        public string CurtaDescricao { get; set; }

        public string LongaDescricao { get; set; }

        public string Cep { get; set; }

        public Guid? FotoDePerfil { get; set; }
        public Guid? FotoDeCapa { get; set; }

        public decimal Nota { get; set; }

        public decimal NotaHigiene { get; set; }
        public decimal NotaPrazer { get; set; }
        public decimal NotaFidelidadeAsFotos { get; set; }
        public decimal NotaEducacao { get; set; }
        public decimal NotaAmbiente { get; set; }
        public decimal NotaPontualidade { get; set; }

        public CaracteristicaVM Caracteristica { get; set; }
        public PreferenciaVM Preferencia { get; set; }

        //public virtual ICollection<AssinaturaVM> Assinaturas { get; set; }

        public virtual ICollection<FotoVM> Fotos { get; set; }

        //public virtual ICollection<VideoVM> Videos { get; set; }
    }
}
