using System;
using System.Collections.Generic;
using Lust.Domain.Assinaturas;
using Lust.Domain.Chats;
using Lust.Domain.Compras;
using Lust.Domain.Core.Models;
//using Microsoft.Spatial;

using Lust.Domain.Sociais;

namespace Lust.Domain.Clientes
{
    public class Cliente : Entity
    {
        

        //public Cliente(Guid id, string name, string email, DateTime birthDate)
        //{
        //    Id = id;
        //    Name = name;
        //    Email = email;
        //    DataNascimento = DataNascimento;
        //}

        // Empty constructor for EF
        public Cliente() {
        }

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


        public string CurtaDescricao {get;set;}

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

        public Caracteristica Caracteristica { get; set; }
        public Preferencia Preferencia { get; set; }

        public DateTime? VencimentoPlanoParaQuemOferece { get; set; }
        public DateTime? VencimentoParaQuemDesfruta { get; set; }

        public virtual ICollection<Assinatura> Assinaturas { get; set; }

        public virtual ICollection<Foto> Fotos { get; set; }

        public virtual ICollection<Video> Videos { get; set; }

        public virtual ICollection<Avaliacao> AvaliacoesFeitas { get; set; }

        public virtual ICollection<Avaliacao> AvaliacoesRecebidas { get; set; }

        public virtual ICollection<Contato> Contatos { get; set; }
        public virtual ICollection<Contato> ContatoDeOutros { get; set; }

        public virtual ICollection<ChatCliente> Chats { get; set; }
        public virtual ICollection<Dialogo> Dialogos { get; set; }
        public virtual ICollection<DialogoLeitura> DialogoLeituras { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }



    }
}