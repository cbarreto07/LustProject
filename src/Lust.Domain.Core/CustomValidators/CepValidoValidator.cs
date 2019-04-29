using FluentValidation.Resources;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lust.Domain.Core.CustomValidators
{
    public class CepValidoValidator : PropertyValidator, IPropertyValidator
    {
        readonly object defaultValueForType;



        public override bool IsAsync
        {
            get { return true; }
        }

        public CepValidoValidator(object defaultValueForType) : base(new LanguageStringSource(nameof(CepValidoValidator)))
        {
            this.defaultValueForType = defaultValueForType;            
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            return IsValidAsync(context, CancellationToken.None).GetAwaiter().GetResult();
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellation)
        {
            if (context.PropertyValue == null || Equals(context.PropertyValue, defaultValueForType))
            {
                return false;
            }
            var Cep = RemoveNaoNumericos(context.PropertyValue.ToString());

            

            var uri = new Uri(string.Format("https://viacep.com.br/ws/{0}/json/", Uri.EscapeDataString(Cep)));


            HttpClient client = new HttpClient();
            try
            {
                var response =await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
            

        }

        private string RemoveNaoNumericos(string text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }



    }


}
