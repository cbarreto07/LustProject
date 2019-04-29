using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.ViewModels
{
    public class PositionVM
    {
        public Guid Id { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
