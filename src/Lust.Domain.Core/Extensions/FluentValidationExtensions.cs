using FluentValidation;
using Lust.Domain.Core.CustomValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Core.Extensions
{
    public static class FluentValidationExtensions
    {
        //public static bool CpfValido(this String str)
        //{
        //    return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        //}

        public static IRuleBuilderOptions<T, string> CpfValido<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CpfValidoValidator(default(string)));
        }

        public static IRuleBuilderOptions<T, string> CepValido<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CepValidoValidator(default(string)));
        }
    }
}
