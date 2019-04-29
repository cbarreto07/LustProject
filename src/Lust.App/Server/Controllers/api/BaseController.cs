
using Lust.Domain.Core.Notifications;
using Lust.Domain.Interfaces;
using Lust.Infra.CrossCutting.AspNetFilters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Lust.App.Server.Controllers.api
{
    
    [ServiceFilter(typeof(ApiExceptionFilter))]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [Produces("application/json")]
    public class BaseController : Controller
    {

        private readonly DomainNotificationHandler _notifications;
        protected Guid UserId { get; set; }

        protected BaseController(INotificationHandler<DomainNotification> notifications,
                                 IUser user)
        {
            _notifications = (DomainNotificationHandler)notifications;


            if (user.IsAuthenticated())
            {
                UserId = user.GetUserId();
            }
        }


        protected new IActionResult Response(object result = null, int totalRows = 0)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result,
                    totalRows = totalRows
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => new ValidationError(n.Key, n.Value))
            });
        }

        protected bool OperacaoValida()
        {
            return (!_notifications.HasNotifications());
        }

        protected void NotificarErroModelInvalida()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(string.Empty, erroMsg);
            }
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _notifications.Handle(new DomainNotification(codigo, mensagem));
        }

        protected void AdicionarErrosIdentity(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                switch (error.Code)
                {
                    case "DuplicateUserName":
                    case "DuplicateEmail":                        
                        NotificarErro("email", "Este email já está sendo utilizado");
                        break;
                                            
                    case "PasswordRequiresDigit":
                        NotificarErro("password", "A senha precisa de pelo menos um número");
                        break;
                    case "PasswordRequiresLower":
                        NotificarErro("password", "A senha precisa de pelo menos uma letra minúcula");
                        break;
                    case "PasswordRequiresNonAlphanumeric":
                        NotificarErro("password", "A senha precisa de pelo menos um caracter especial");
                        break;
                    case "PasswordRequiresUpper":
                        NotificarErro("password", "A senha precisa de pelo menos uma letra maiuscula");
                        break;
                    case "PasswordTooShort":
                        NotificarErro("password", "A senha precisa está muito curta");
                        break;
                    default:
                        NotificarErro(error.Code, error.Description);
                        break;


                }

                
            }
        }
    }
}
