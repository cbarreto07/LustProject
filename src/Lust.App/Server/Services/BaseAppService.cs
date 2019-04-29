using Lust.Domain.Core.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.Services
{
    public abstract class BaseAppService
    {
        protected readonly DomainNotificationHandler _notifications;
        protected BaseAppService (INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected async Task NotificarValidacoesErro(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                await _notifications.Handle(new DomainNotification(error.PropertyName, error.ErrorMessage), new System.Threading.CancellationToken());
            }
        }

        protected async Task NotificarErro(string key, string value)
        {
            await _notifications.Handle(new DomainNotification(key,value), new System.Threading.CancellationToken());
        }

    }
}
