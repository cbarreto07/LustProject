using System.Collections.Generic;
using System.Linq;
using Lust.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Lust.Infra.CrossCutting.AspNetHelper
{
    public static class Helpers
    {
        public static string JsonSerialize(object obj)
        {
            return JsonConvert.SerializeObject(obj,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                            StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });
        }

        public static IActionResult Render(this Controller ctrl, ExternalLoginStatus status = ExternalLoginStatus.Ok, string email = "", string nome = "")
        {
            if (status == ExternalLoginStatus.Ok)
            {
                return ctrl.LocalRedirect("~/");
            }
            return ctrl.LocalRedirect($"~/?externalLoginStatus={(int)status}&nome={nome}&email={email}");
            // return RedirectToAction("Index", "Home", new { externalLoginStatus = (int)status });
        }
    }
}