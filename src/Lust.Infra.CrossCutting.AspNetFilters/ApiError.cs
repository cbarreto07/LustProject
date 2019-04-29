using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Collections.Generic;

namespace Lust.Infra.CrossCutting.AspNetFilters { 

    public class ApiError
    {
        public string Message { get; set; }
        public bool isError { get; set; }
        public string detail { get; set; }
        public bool success { get; set; }
        public List<ValidationError> errors { get; set; }

        public ApiError(string message)
        {
            this.Message = message;
            isError = true;
        }

        public ApiError(ModelStateDictionary modelState)
        {

            //TODO revisar
            this.isError = true;
            success = false;
            Message = "Validation Failed";
            errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
    }
}