﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetEscapades.AspNetCore.SecurityHeaders;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Lust.Infra.Data.Context;
using Lust.Infra.CrossCutting.Identity.Data;
using System.Linq;

namespace Lust.App.Server.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private static readonly String AUTH_QUERY_STRING_KEY = "access_token";

        // https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders
        public static IApplicationBuilder AddCustomSecurityHeaders(this IApplicationBuilder app)
        {
            var env = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();

            var policyCollection = new HeaderPolicyCollection()
                   .AddFrameOptionsDeny()
                   .AddXssProtectionBlock()
                   .AddContentTypeOptionsNoSniff()
                   .AddStrictTransportSecurityMaxAge(maxAgeInSeconds: 60 * 60 * 24 * 365) // maxage = one year in seconds
                   .AddReferrerPolicyOriginWhenCrossOrigin()
                   .RemoveServerHeader()
                   .AddContentSecurityPolicy(builder =>
                    {
                        if (env.IsProduction())
                        {
                            builder.AddUpgradeInsecureRequests(); // upgrade-insecure-requests
                        }

                        // builder.AddReportUri() // report-uri: https://report-uri.com
                        //     .To("https://report-uri.com");

                        builder.AddDefaultSrc()
                            .Self();

                        // Allow AJAX, WebSocket and EventSource connections to:
                        var socketUrl = Startup.Configuration["HostUrl"].ToString().Replace("http://", "ws://", StringComparison.OrdinalIgnoreCase).Replace("https://", "wss://", StringComparison.OrdinalIgnoreCase);

                        builder.AddConnectSrc()
                            .Self()
                            .From(socketUrl);

                        builder.AddFontSrc() // font-src 'self'
                            .Self()
                            .Data();

                        builder.AddObjectSrc() // object-src 'none'
                            .None();

                        builder.AddFormAction() // form-action 'self'
                            .Self();

                        builder.AddImgSrc() // img-src https:
                            .Self()
                            .Data();

                        // builder.AddScriptSrc() // script-src 'self'
                        //     .Self();

                        // builder.AddStyleSrc() // style-src 'self'
                        //     .Self();

                        builder.AddUpgradeInsecureRequests(); // upgrade-insecure-requests
                        builder.AddCustomDirective("script-src", "'self' 'unsafe-inline' 'unsafe-eval'");
                        builder.AddCustomDirective("style-src", "'self' 'unsafe-inline' 'unsafe-eval'");

                        builder.AddMediaSrc()
                            .Self();

                        builder.AddFrameAncestors() // frame-ancestors 'none'
                            .None();

                        builder.AddFrameSource()
                            .None();

                        // You can also add arbitrary extra directives: plugin-types application/x-shockwave-flash"
                        // builder.AddCustomDirective("plugin-types", "application/x-shockwave-flash");

                    });

            app.UseSecurityHeaders(policyCollection);
            return app;
        }
        //public static IApplicationBuilder UseCustomSwaggerApi(this IApplicationBuilder app)
        //{
        //    // Enable middleware to serve generated Swagger as a JSON endpoint
        //    app.UseSwagger();
        //    // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
        //    app.UseSwaggerUI(c =>
        //    {
        //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
        //    });

        //    return app;
        //}
        public static IApplicationBuilder AddDevMiddlewares(this IApplicationBuilder app)
        {
            var env = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();
            var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();

            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(Startup.Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                // NOTE: For SPA swagger needs adding before MVC
               // app.UseCustomSwaggerApi();
            }

            return app;
        }

        public static IApplicationBuilder AddCustomLocalization(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            return app;
        }

        public static IApplicationBuilder SetupMigrations(this IApplicationBuilder app)
        {
            // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
            try
            {
                var context = app.ApplicationServices.GetService<LustContext>();
                context.Database.Migrate();

                var contextIdentity = app.ApplicationServices.GetService<ApplicationDbContext>();
                contextIdentity.Database.Migrate();
            }
            catch (Exception) { }
            return app;
        }

        public static void UseJwtSignalRAuthentication(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                if (string.IsNullOrWhiteSpace(context.Request.Headers["Authorization"]))
                {
                    try
                    {
                        if (context.Request.QueryString.HasValue)
                        {

                            var token = context.Request.QueryString.Value
                                .Split('&')
                                .SingleOrDefault(x => x.Contains(AUTH_QUERY_STRING_KEY))?
                                .Split('=')
                                .Skip(1)
                                .First();

                            if (!string.IsNullOrWhiteSpace(token))
                            {
                                context.Request.Headers.Add("Authorization", new[] { $"Bearer {token}" });
                            }

                        }

                    }
                    catch
                    {
                        // if multiple headers it may throw an error.  Ignore both.
                    }
                }
                await next.Invoke();
            });

        }
    


}
}
