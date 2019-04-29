using System;
using System.Collections.Generic;

using Lust.Infra.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Lust.App.Server.Middlewares.EntityFrameworkLocalizer
{
    public class EFStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly LustContext _context;

        public EFStringLocalizerFactory(IServiceScopeFactory serviceScopeFactory)
        {
            var serviceScope = serviceScopeFactory.CreateScope();
            _context = serviceScope.ServiceProvider.GetService<LustContext>();


        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new EFStringLocalizer(_context);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new EFStringLocalizer(_context);
        }
    }
}
