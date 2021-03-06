﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.ViewModels
{
    public class PerfilVM
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }



        public int Idade { get; set; }


        public string CurtaDescricao { get; set; }

        public string LongaDescricao { get; set; }

        public Decimal Valor30min { get; set; }

        public Decimal Valor1Hora { get; set; }

        public Decimal Valor2horas { get; set; }

        public Decimal ValorPernoite { get; set; }

        public Guid? FotoDeCapa { get; set; }

        public string FotoDeCapaThumbnail { get; set; }

        public Guid? FotoDePerfil { get; set; }
        public string FotoDePerfilThumbnail { get; set; }

        public Decimal Nota { get; set; }

        public decimal Distancia { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public decimal NotaHigiene { get; set; }
        public decimal NotaPrazer { get; set; }
        public decimal NotaFidelidadeAsFotos { get; set; }
        public decimal NotaEducacao { get; set; }
        public decimal NotaAmbiente { get; set; }
        public decimal NotaPontualidade { get; set; }

        public bool LocalProprio { get; set; }

        public List<FotoPerfilVM> Fotos { get; set; }
        public List<DotePerfilVM> Dotes { get; set; }
    }
}
