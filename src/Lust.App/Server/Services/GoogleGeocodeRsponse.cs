using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.Services
{
    public class GoogleGeocodeRsponse
    {
        public List<Result> Results { get; set; }
    }


    public class Result
    {
        public Geometry Geometry { get; set; }
    }

    public class Geometry
    {
        public Location Location;
    }

    public class Location
    {
        public float lat { get; set; }
        public float lng { get; set; }
        
    }
}
