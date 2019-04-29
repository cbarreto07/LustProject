using System;
using System.Linq;
using Lust.App.Server.Extensions;
using Lust.Infra.CrossCutting.Identity.Data;
using Lust.Infra.Data.Context;
using Lust.Infra.Files.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lust.App.Server
{
    public class ProcessDbCommands
    {
        public static void Process(string[] args, IWebHost host)
        {
            var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));
            var seedService = (SeedDbData)host.Services.GetService(typeof(SeedDbData));

            using (var scope = services.CreateScope())
            {
                var storage = scope.ServiceProvider.GetRequiredService<IAzureBlobStorage>();
                //storage.UploadAsync( @"1x1/teste.jpg", @"D:\Workspace\LustProject\src\Lust.App\ClientApp\src\assets\img\icon180x180.png");

                var db = GetLustContext(scope);
                var dbIdentity = GetLustContext(scope);
                // if (args.Contains("dropdb"))
                // {
                //     Console.WriteLine("Dropping database");
                //     db.Database.EnsureDeleted();
                // }

                // if (args.Contains("migratedb"))
                // {
                // Console.WriteLine("Migrating database");
                // db.Database.Migrate();
                // }

                // if (args.Contains("seeddb"))
                // {
                Console.WriteLine("Seeding database");
                //db.Seed(host);
                if (db.AllMigrationsApplied() && dbIdentity.AllMigrationsApplied())
                {
                    var seed = new SeedDbData(host);
                }

                // }
            }
        }

        private static LustContext GetLustContext(IServiceScope services)
        {
            var db = services.ServiceProvider.GetRequiredService<LustContext>();
            return db;
        }

        private static ApplicationDbContext GetIdentityContext(IServiceScope services)
        {
            var db = services.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            return db;
        }


    }
}