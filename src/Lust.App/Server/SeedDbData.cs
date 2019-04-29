using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lust.App;
using Lust.App.Server.Interfaces;
using Lust.Domain.Clientes;
using Lust.Domain.Localises;
using Lust.Infra.CrossCutting.Identity.Data;
using Lust.Infra.CrossCutting.Identity.Models;
using Lust.Infra.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Core;
using OpenIddict.Models;

namespace Lust.App.Server
{
    public class SeedDbData
    {

        readonly string[] ceps = new string[] {
            //bh
            "31320240", "31340150", "30380230", "31510090", "30451039", "30431180", "31844000", "30350285", "31998010", "31998612","32310660",
            //contagem
            "32013460","32241200","32070510","32150140","32185730","32046010","32115100","32186310","32215200","32187070","32242200","32371050","32052016","32240292","32115020","32010332","32140140","32180500","32341050","32240360","32041730","32041370","32260280","32230160",
 };
        
        readonly IClienteAppService _clienteService;
        readonly LustContext _context;
        readonly ApplicationDbContext _identityContext;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public SeedDbData(IWebHost host)
        {
            var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));
            var serviceScope = services.CreateScope();
            _hostingEnv = serviceScope.ServiceProvider.GetService<IHostingEnvironment>();
            _roleManager = serviceScope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();
            _userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
            _context = serviceScope.ServiceProvider.GetService<LustContext>();
            _clienteService = serviceScope.ServiceProvider.GetService<IClienteAppService>();
            _identityContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>(); ;
            CreateRoles(); // Add roles
            CreateUsers(); // Add users
            AddLocalisedData();
            AddOpenIdConnectOptions(serviceScope, CancellationToken.None).GetAwaiter().GetResult();
            AddClientes();
        }

        private void CreateRoles()
        {
            var rolesToAdd = new List<ApplicationRole>(){
                new ApplicationRole { Name= "Admin", Description = "Full rights role"},
                new ApplicationRole { Name= "User", Description = "Limited rights role"}
            };
            foreach (var role in rolesToAdd)
            {
                if (!_roleManager.RoleExistsAsync(role.Name).Result)
                {
                    _roleManager.CreateAsync(role).Result.ToString();                    
                }
            }
        }
        private void CreateUsers()
        {
            if (!_identityContext.Users.Any())
            {
                var user = new ApplicationUser { UserName = "admin@admin.com", Name = "Admin", Email = "admin@admin.com", EmailConfirmed = true, CreatedDate = DateTime.Now, IsEnabled = true };
                _userManager.CreateAsync(user, "P@ssw0rd!").Result.ToString();
                _userManager.AddToRoleAsync(_userManager.FindByNameAsync("admin@admin.com").GetAwaiter().GetResult(), "Admin").Result.ToString();

                // _userManager.CreateAsync(new ApplicationUser { UserName = "user@user.com", Name = "user", Email = "user@user.com", EmailConfirmed = true, CreatedDate = DateTime.Now, IsEnabled = true }, "P@ssw0rd!").Result.ToString();
                //_userManager.AddToRoleAsync(_userManager.FindByNameAsync("user@user.com").GetAwaiter().GetResult(), "User").Result.ToString();
                var model = new ViewModels.ClienteViewModel()
                {
                    Id = Guid.Parse(user.Id),
                    Nome = "Admin",
                    Email = user.Email,
                    Celular = "31988616936",
                    Cpf = "05627810645",
                    DataNascimento = new DateTime(1983, 02, 13),
                    Cep = "35970000",
                    CurtaDescricao = "Olá, eu sou o admin",
                    EstaOferecendo = true,
                    Genero = Domain.Clientes.EnumGenero.Homem,
                    LongaDescricao = "essa é minha descrição longa",
                    Nota = 1,                    
                    NotaAmbiente = 1,
                    NotaEducacao = 1,
                    NotaFidelidadeAsFotos = 1,
                    NotaHigiene = 1,
                    NotaPontualidade = 1,
                    NotaPrazer = 1,

                };

                _clienteService.RegistrarAsync(model).GetAwaiter().GetResult();

            }
        }

        private void AddClientes()
        {
            if (!_context.Clientes.Any(q => q.Nome.Contains("garota"))){
                Random random = new Random();
                for (var i = 1; i <= 26; i++)
                {

                    var user = new ApplicationUser { UserName = $"garota{i}@admin.com", Name = $"garota{i}", Email = $"garota{i}@lust.com", EmailConfirmed = true, CreatedDate = DateTime.Now, IsEnabled = true };
                    _userManager.CreateAsync(user, "P@ssw0rd!").Result.ToString();

                    var model = new ViewModels.ClienteViewModel()
                    {
                        Id = Guid.Parse(user.Id),
                        Nome = $"garota{i}",
                        Email = user.Email,
                        Celular = "31988616936",
                        Cpf = "05627810645",
                        DataNascimento = new DateTime(2001 - i, 01, 01),
                        Cep = ceps[i - 1],
                        CurtaDescricao = "Olá, eu sou a garota " + i,
                        EstaOferecendo = true,
                        Genero = Domain.Clientes.EnumGenero.Mulher,
                        LongaDescricao = "essa é minha descrição longa",
                        Nota = (decimal)random.NextDouble(),
                        FotoDeCapa = Guid.NewGuid(),
                        FotoDePerfil = Guid.NewGuid(),
                        NotaAmbiente = (decimal)random.NextDouble(),
                        NotaEducacao = (decimal)random.NextDouble(),
                        NotaFidelidadeAsFotos = (decimal)random.NextDouble(),
                        NotaHigiene = (decimal)random.NextDouble(),
                        NotaPontualidade = (decimal)random.NextDouble(),
                        NotaPrazer = (decimal)random.NextDouble(),                         
                         
                    };

                    _clienteService.RegistrarAsync(model).GetAwaiter().GetResult();


                    var cliente = _context.Clientes
                        .Include(q=>q.Caracteristica)
                        .Single(q => q.Id == model.Id);

                    
                    cliente.Caracteristica.LocalProprio = false;
                    cliente.Caracteristica.Valor1Hora = 100 + 10 * i;
                    cliente.Caracteristica.Valor2horas = 200 + 10 * i;
                    cliente.Caracteristica.Valor30min = 50 + 10 * i;
                    cliente.Caracteristica.ValorPernoite = 500 + 10 * i;
                    
                    _context.SaveChanges();

                    var stream = new FileStream(Directory.GetCurrentDirectory() + $"\\extra\\fotos\\{i}.jpg", FileMode.Open);
                    _clienteService.AddFotoAsync(model.Id, model.FotoDePerfil.Value,"minha foto de perfil", stream).GetAwaiter().GetResult();
                    stream.Close();

                    stream = new FileStream(Directory.GetCurrentDirectory() + $"\\extra\\fotos\\capa.jpg", FileMode.Open);
                    _clienteService.AddFotoAsync(model.Id, model.FotoDeCapa.Value, "minha foto de capa", stream).GetAwaiter().GetResult();
                    stream.Close();

                    stream = new FileStream(Directory.GetCurrentDirectory() + $"\\extra\\fotos\\extra.jpg", FileMode.Open);
                    _clienteService.AddFotoAsync(model.Id, Guid.NewGuid(), "minha foto de extra", stream).GetAwaiter().GetResult();
                    stream.Close();

                    
                }

                
            }
        }

        private void AddLocalisedData()
        {
            if (!_context.Cultures.Any())
            {
                _context.Cultures.AddRange(
                    new Culture
                    {
                        Name = "pt-BR",
                        Resources = new List<Resource>() {
                            new Resource { Key = "app_title", Value = "Lust Book" },
                            new Resource { Key = "app_description", Value = "Single page application using aspnet core and angular" },
                            new Resource { Key = "app_nav_home", Value = "Home" },
                            new Resource { Key = "app_nav_chat", Value = "Chat" },
                            new Resource { Key = "app_nav_examples", Value = "Examplos" },
                            new Resource { Key = "app_nav_register", Value = "Registro" },
                            new Resource { Key = "app_nav_login", Value = "Entrar" },
                            new Resource { Key = "app_nav_logout", Value = "Sair" },
                        }
                    },
                    new Culture
                    {
                        Name = "en-US",
                        Resources = new List<Resource>() {
                            new Resource { Key = "app_title", Value = "Lust Book" },
                            new Resource { Key = "app_description", Value = "Single page application using aspnet core and angular" },
                            new Resource { Key = "app_nav_home", Value = "Home" },
                            new Resource { Key = "app_nav_chat", Value = "Chat" },
                            new Resource { Key = "app_nav_examples", Value = "Examples" },
                            new Resource { Key = "app_nav_register", Value = "Register" },
                            new Resource { Key = "app_nav_login", Value = "Login" },
                            new Resource { Key = "app_nav_logout", Value = "Logout" },
                        }
                    }
                    //new Culture
                    //{
                    //    Name = "fr-FR",
                    //    Resources = new List<Resource>() {
                    //        new Resource { Key = "app_title", Value = "Lust Book" },
                    //        new Resource { Key = "app_description", Value = "Application d'une seule page utilisant aspnet core et angular" },
                    //        new Resource { Key = "app_nav_home", Value = "Accueil" },
                    //        new Resource { Key = "app_nav_chat", Value = "Bavarder" },
                    //        new Resource { Key = "app_nav_examples", Value = "Exemples" },
                    //        new Resource { Key = "app_nav_register", Value = "registre" },
                    //        new Resource { Key = "app_nav_login", Value = "S'identifier" },
                    //        new Resource { Key = "app_nav_logout", Value = "Connectez - Out" },
                    //    }
                    //}
                    );

                _context.SaveChanges();
            }

        }

        private async Task AddOpenIdConnectOptions(IServiceScope services, CancellationToken cancellationToken)
        {
            var manager = services.ServiceProvider.GetService<OpenIddictApplicationManager<OpenIddictApplication>>();

            if (await manager.FindByClientIdAsync("lustspa", cancellationToken) == null)
            {
                var host = Startup.Configuration["HostUrl"].ToString();

                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = "lustspa",
                    DisplayName = "LustSpa",
                    PostLogoutRedirectUris = { new Uri($"{host}signout-oidc") },
                    RedirectUris = { new Uri(host), new Uri(host+ "login/external") },
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.Implicit,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken
                    }
                };

                await manager.CreateAsync(descriptor, cancellationToken);
            }

            // if (await manager.FindByClientIdAsync("resource-server-1", cancellationToken) == null)
            // {
            //     var descriptor = new OpenIddictApplicationDescriptor
            //     {
            //         ClientId = "resource-server-1",
            //         ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342"
            //     };

            //     await manager.CreateAsync(descriptor, cancellationToken);
            // }

            // if (await manager.FindByClientIdAsync("resource-server-2", cancellationToken) == null)
            // {
            //     var descriptor = new OpenIddictApplicationDescriptor
            //     {
            //         ClientId = "resource-server-2",
            //         ClientSecret = "C744604A-CD05-4092-9CF8-ECB7DC3499A2"
            //     };

            //     await manager.CreateAsync(descriptor, cancellationToken);
            // }
        }
    }
}
