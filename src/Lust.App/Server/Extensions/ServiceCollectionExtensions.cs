﻿using AspNet.Security.OpenIdConnect.Primitives;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Hosting;
using Lust.Infra.Data.Context;
using Lust.App;
using Lust.Infra.CrossCutting.Identity.Models;
using Lust.Infra.CrossCutting.Identity.Data;
using AutoMapper;

using Lust.Domain.Assinaturas.Interfaces;
using Lust.Domain.Clientes.Interfaces;
using Lust.Domain.Core.Notifications;
using Lust.Domain.Interfaces;
using Lust.Domain.Planos.Interfaces;
using Lust.Domain.Sociais.Interfaces;
using Lust.Infra.CrossCutting.AspNetFilters;
using Lust.Infra.CrossCutting.Identity.Authorization;
using Lust.Infra.CrossCutting.Identity.Services;


using Lust.Infra.Data.Repository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Logging;
using Lust.App.Server.Services;
using Lust.App.Server.Interfaces;
using Lust.App.Server.Middlewares.EntityFrameworkLocalizer;
using Lust.Infra.Files.Storage;
using Lust.Infra.Files.Image;
using Lust.App.Server.SignalR;
using Lust.Domain.Chats.Interfaces;
using System.IO;
using Microsoft.AspNetCore.SignalR;

namespace Lust.App.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // https://github.com/aspnet/JavaScriptServices/tree/dev/src/Microsoft.AspNetCore.SpaServices#debugging-your-javascripttypescript-code-when-it-runs-on-the-server
        // Url to visit:
        // chrome-devtools://devtools/bundled/inspector.html?experiments=true&v8only=true&ws=127.0.0.1:9229/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
        public static IServiceCollection AddPreRenderDebugging(this IServiceCollection services, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                services.AddNodeServices(options =>
                {
                    options.LaunchWithDebugging = true;
                    options.DebuggingPort = 9229;
                });
            }

            return services;
        }
        public static IServiceCollection AddCustomizedMvc(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ModelValidationFilter));
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();

            return services;
        }
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // options for user and password can be set here
                // options.Password.RequiredLength = 4;
                // options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
        public static IServiceCollection AddCustomOpenIddict(this IServiceCollection services, IHostingEnvironment env)
        {
            // Configure Identity to use the same JWT claims as OpenIddict instead
            // of the legacy WS-Federation claims it uses by default (ClaimTypes),
            // which saves you from doing the mapping in your authorization controller.
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
               
            });

            // Register the OpenIddict services.
            services.AddOpenIddict(options =>
            {
                // Register the Entity Framework stores.
               options.AddEntityFrameworkCoreStores<ApplicationDbContext>();

                // Register the ASP.NET Core MVC binder used by OpenIddict.
                // Note: if you don't call this method, you won't be able to
                // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                options.AddMvcBinders();

                // Enable the token endpoint.
                // Form password flow (used in username/password login requests)
                options.EnableTokenEndpoint("/connect/token");

                // Enable the authorization endpoint.
                // Form implicit flow (used in social login redirects)
                options.EnableAuthorizationEndpoint("/connect/authorize");

                // Enable the password and the refresh token flows.
                options.AllowPasswordFlow()
                       .AllowRefreshTokenFlow()
                       .AllowImplicitFlow(); // To enable external logins to authenticate

                options.SetAccessTokenLifetime(TimeSpan.FromMinutes(30));
                options.SetIdentityTokenLifetime(TimeSpan.FromMinutes(30));
                options.SetRefreshTokenLifetime(TimeSpan.FromDays(7));
                // During development, you can disable the HTTPS requirement.
                if (env.IsDevelopment())
                {
                    options.DisableHttpsRequirement();
                }

                // Note: to use JWT access tokens instead of the default
                // encrypted format, the following lines are required:
                //
                // options.UseJsonWebTokens();
                //options.AddEphemeralSigningKey();
                options.AddSigningCertificate(new FileStream(Directory.GetCurrentDirectory() + "/extra/lust.pfx", FileMode.Open), "@Lu$t69");
            });

            // If you prefer using JWT, don't forget to disable the automatic
            // JWT -> WS-Federation claims mapping used by the JWT middleware:
            //
            // JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            // JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            //
            // services.AddAuthentication()
            //     .AddJwtBearer(options =>
            //     {
            //         options.Authority = "http://localhost:54895/";
            //         options.Audience = "resource_server";
            //         options.RequireHttpsMetadata = false;
            //         options.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             NameClaimType = OpenIdConnectConstants.Claims.Subject,
            //             RoleClaimType = OpenIdConnectConstants.Claims.Role
            //         };
            //     });

            // Alternatively, you can also use the introspection middleware.
            // Using it is recommended if your resource server is in a
            // different application/separated from the authorization server.
            //
            // services.AddAuthentication()
            //     .AddOAuthIntrospection(options =>
            //     {
            //         options.Authority = new Uri("http://localhost:54895/");
            //         options.Audiences.Add("resource_server");
            //         options.ClientId = "resource_server";
            //         options.ClientSecret = "875sqd4s5d748z78z7ds1ff8zz8814ff88ed8ea4z4zzd";
            //         options.RequireHttpsMetadata = false;
            //     });

            services.AddAuthentication(options =>
            {
                // This will override default cookies authentication scheme
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddOAuthValidation()
               // https://console.developers.google.com/projectselector/apis/library?pli=1
               .AddGoogle(options =>
               {
                   options.ClientId = Startup.Configuration["Authentication:Google:ClientId"];
                   options.ClientSecret = Startup.Configuration["Authentication:Google:ClientSecret"];
               })
               // https://developers.facebook.com/apps
               .AddFacebook(options =>
               {
                   options.AppId = Startup.Configuration["Authentication:Facebook:AppId"];
                   options.AppSecret = Startup.Configuration["Authentication:Facebook:AppSecret"];
               });
               //// https://apps.twitter.com/
               //.AddTwitter(options =>
               //{
               //    options.ConsumerKey = Startup.Configuration["Authentication:Twitter:ConsumerKey"];
               //    options.ConsumerSecret = Startup.Configuration["Authentication:Twitter:ConsumerSecret"];
               //})
               //// https://apps.dev.microsoft.com/?mkt=en-us#/appList
               //.AddMicrosoftAccount(options =>
               //{
               //    options.ClientId = Startup.Configuration["Authentication:Microsoft:ClientId"];
               //    options.ClientSecret = Startup.Configuration["Authentication:Microsoft:ClientSecret"];
               //})
               //// Note: Below social providers are supported through this open source library:
               //// https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers

               //// https://www.linkedin.com/secure/developer?newapp=
               //.AddLinkedIn(options =>
               //{
               //    options.ClientId = Startup.Configuration["Authentication:LinkedIn:ClientId"];
               //    options.ClientSecret = Startup.Configuration["Authentication:LinkedIn:ClientSecret"];

               //})
               //// https://github.com/settings/developers
               //.AddGitHub(options =>
               //{
               //    options.ClientId = Startup.Configuration["Authentication:Github:ClientId"];
               //    options.ClientSecret = Startup.Configuration["Authentication:Github:ClientSecret"];

               //})
               //// https://developer.paypal.com/developer/applications
               //.AddPaypal(options =>
               //   {
               //       options.ClientId = Startup.Configuration["Authentication:Paypal:ClientId"];
               //       options.ClientSecret = Startup.Configuration["Authentication:Paypal:ClientSecret"];
               //       if (env.IsDevelopment())
               //       {
               //           options.AuthorizationEndpoint = "https://www.sandbox.paypal.com/webapps/auth/protocol/openidconnect/v1/authorize";
               //           options.TokenEndpoint = "https://api.sandbox.paypal.com/v1/identity/openidconnect/tokenservice";
               //           options.UserInformationEndpoint = "https://api.sandbox.paypal.com/v1/identity/openidconnect/userinfo?schema=openid";
               //       }
               //   })
               //// https://developer.yahoo.com
               //.AddYahoo(options =>
               //{
               //    options.ClientId = Startup.Configuration["Authentication:Yahoo:ClientId"];
               //    options.ClientSecret = Startup.Configuration["Authentication:Yahoo:ClientSecret"];
               //})
               //// https://stackapps.com/apps/oauth/
               //.AddStackExchange(options =>
               //{
               //    options.ClientId = Startup.Configuration["Authentication:StackExchange:ClientId"];
               //    options.ClientSecret = Startup.Configuration["Authentication:StackExchange:ClientSecret"];
               //})
               //// https://stackapps.com/apps/oauth/
               //.AddAmazon(options =>
               //{
               //    options.ClientId = Startup.Configuration["Authentication:Amazon:ClientId"];
               //    options.ClientSecret = Startup.Configuration["Authentication:Amazon:ClientSecret"];
               //});

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContextPool<LustContext>(options =>
            {
                string useSqLite = Startup.Configuration["Data:useSqLite"];
                string useInMemory = Startup.Configuration["Data:useInMemory"];
                if (useInMemory.ToLower() == "true")
                {
                    options.UseInMemoryDatabase("LustDb"); // Takes database name
                }
                else if (useSqLite.ToLower() == "true")
                {
                    options.UseSqlite(Startup.Configuration["Data:SqlLiteConnectionString"]);
                }
                else
                {
                    options.UseSqlServer(Startup.Configuration["Data:SqlServerConnectionString"]);
                }
                
            });

            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                string useSqLite = Startup.Configuration["Data:useSqLite"];
                string useInMemory = Startup.Configuration["Data:useInMemory"];
                if (useInMemory.ToLower() == "true")
                {
                    options.UseInMemoryDatabase("LustDb"); // Takes database name
                }
                else if (useSqLite.ToLower() == "true")
                {
                    options.UseSqlite(Startup.Configuration["Data:SqlLiteConnectionString"]);
                }
                else
                {
                    options.UseSqlServer(Startup.Configuration["Data:SqlServerConnectionString"]);
                }
                options.UseOpenIddict();
            });



            return services;
        }

        public static IServiceCollection AddCustomLocalization(this IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("pt-BR"),
                        new CultureInfo("en-US"),
                        new CultureInfo("fr-FR")
                    };

                    opts.DefaultRequestCulture = new RequestCulture("pt-BR");
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;
                });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            return services;
        }
        public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
        {
            // New instance every time, only configuration class needs so its ok
            services.AddSingleton<IStringLocalizerFactory, EFStringLocalizerFactory>();
            //services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<IApplicationDataService, ApplicationDataService>();
            //services.AddTransient<LustContext>();
            //services.AddScoped<UserResolverService>();
            services.AddScoped<ApiExceptionFilter>();


            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            // ASP.NET Authorization Polices
            //services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>(); ;

            // Application
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            services.AddScoped<IClienteAppService, ClienteAppService>();
            services.AddScoped<IDoteAppService, DoteAppService>();
            services.AddScoped<IPlanoAppService, PlanoAppService>();
            services.AddScoped<IChatClienteAppService, ChatClienteAppService>();
            services.AddScoped<ILocationAppService, LocationAppService>();

            

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Infra - Data
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IDoteRepository, DoteRepository>();            
            services.AddScoped<IAssinaturaRepository, AssinaturaRepository>();
            services.AddScoped<IPlanoRepository, PlanoRepository>();
            services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();

            services.AddScoped<LustContext>();



            // Infra - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            services.AddScoped<ApplicationDbContext>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();

            // Infra - Filtros
            services.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
            services.AddScoped<ILogger<GlobalActionLogger>, Logger<GlobalActionLogger>>();
            services.AddScoped<GlobalExceptionHandlingFilter>();
            services.AddScoped<GlobalActionLogger>();

            //Infra -  Files

            services.AddTransient<IAzureBlobStorage>(factory =>
            {
                return new AzureBlobStorage(new AzureBlobSetings(
                    storageAccount: Startup.Configuration["Blob:StorageAccount"],
                    storageKey: Startup.Configuration["Blob:StorageKey"],
                    containerName: Startup.Configuration["Blob:ContainerName"]));
            });

            //irá manter o historico de links já criados
            services.AddSingleton<ImageLinkCache>();
            services.AddScoped<IImageStorage, ImageStorage>();

            //substitui o provedor de usuario do signalR
            services.Remove(ServiceDescriptor.Singleton(typeof(IUserIdProvider), typeof(DefaultUserIdProvider)));
            services.AddSingleton(typeof(IUserIdProvider), typeof(LustUserIdProvider));


            return services;
        }

    }
}
