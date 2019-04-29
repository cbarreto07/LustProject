using Lust.App.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lust.App.Server.Services
{
    public class LocationAppService : ILocationAppService
    {
        public LocationAppService()
        {

        }

       

        public async Task<Location> GetLocation(string cep)
        {
            var location = new Location() { lat = -23.613099f, lng = -46.650565f };
            
            cep = RemoveNaoNumericos(cep);
            string address = cep + ",Brasil";
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key=AIzaSyAR0oRM_cgjLRKJ3tJIG4ylnBdKnKG3x_Y&address={0}&sensor=false", Uri.EscapeDataString(address));
            try
            {
                WebRequest request = WebRequest.Create(requestUri);
                WebResponse response = await request.GetResponseAsync();
                XDocument xdoc = XDocument.Load(response.GetResponseStream());

                XElement result = xdoc.Element("GeocodeResponse").Element("result");
                XElement locationElement = result.Element("geometry").Element("location");
                float lat = float.Parse(locationElement.Element("lat").Value, CultureInfo.InvariantCulture.NumberFormat);
                float lng = float.Parse(locationElement.Element("lng").Value, CultureInfo.InvariantCulture.NumberFormat);
                location.lat = lat;
                location.lng = lng;
            }
            catch { } // se der erro eu retor o padrao
            return location;
        }

        private string RemoveNaoNumericos(string text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }
    }
}
