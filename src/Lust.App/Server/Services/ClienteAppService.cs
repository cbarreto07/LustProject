using AutoMapper;
using FluentValidation.Results;
using Lust.App.Server.Interfaces;
using Lust.App.Server.ViewModels;
using Lust.Domain.Clientes;
using Lust.Domain.Clientes.Interfaces;
using Lust.Domain.Clientes.Validations;
using Lust.Domain.Core.Notifications;
using Lust.Domain.Interfaces;
using Lust.Infra.Files.Image;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.Services
{
    public class ClienteAppService : IClienteAppService
    {

        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;
        private readonly DomainNotificationHandler _notifications;
        private readonly IUser _user;
        private readonly ILocationAppService _locationAppService;
        private readonly IImageStorage _imageStorage;

        public ClienteAppService(IMapper mapper, IClienteRepository clienteRepository, INotificationHandler<DomainNotification> notifications, IUser user, ILocationAppService locationAppService, IImageStorage imageStorage)
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
            _notifications = (DomainNotificationHandler)notifications;
            _user = user;
            _locationAppService = locationAppService;
            _imageStorage = imageStorage;

        }

        public async Task RegistrarAsync(Guid Id, string nome, string email, string celular, string cpf, DateTime dataNascimento, string cep)
        {
            var cliente = new Cliente()
            {
                Id = Id,
                Nome = nome, 
                Email = email,
                Celular = celular,
                Cpf = cpf,
                DataNascimento = dataNascimento,
                Cep = cep
            };

            var ValidationResult = new RegisterNewClienteValidation().Validate(cliente);
            if (!ValidationResult.IsValid)
            {
                NotificarValidacoesErro(ValidationResult);
                return;
            }

            var location = await _locationAppService.GetLocation(cliente.Cep);
            cliente.Latitude = location.lat;
            cliente.Longitude = location.lng;
            cliente.CurtaDescricao = "Olá, meu nome é " + cliente.Nome;
            cliente.LongaDescricao = "Olá, meu nome é " + cliente.Nome;
            // TODO:
            // Validacoes de negocio!
            cliente.Preferencia = new Preferencia()
            {
                Distancia = 150,
                IdadeMaxima = 65,
                IdadeMinima = 18,
                PrecoMinimo = 0,
                PrecoMaximo = 5000


            };
            cliente.Caracteristica = new Caracteristica()
            {
                LocalProprio = false,
                Valor1Hora = 0,
                Valor2horas = 0,
                Valor30min = 0,
                ValorPernoite = 0
            };


            _clienteRepository.Add(cliente);
            await _clienteRepository.SaveChangesAsync();
        }


        public async Task RegistrarAsync(ClienteViewModel clienteViewModel)
        {
            var cliente = _mapper.Map<Cliente>(clienteViewModel);
            var ValidationResult = new RegisterNewClienteValidation().Validate(cliente);
            if (!ValidationResult.IsValid)
            {
                NotificarValidacoesErro(ValidationResult);
                return ;
            }

            // TODO:
            // Validacoes de negocio!

            var location = await _locationAppService.GetLocation(cliente.Cep);
            cliente.Latitude = location.lat;
            cliente.Longitude = location.lng;
            cliente.Preferencia = new Preferencia()
            {
                Distancia = 150,
                IdadeMaxima = 65,
                IdadeMinima = 18,
                PrecoMinimo = 0,
                PrecoMaximo = 5000


            };
            cliente.Caracteristica = new Caracteristica()
            {
                LocalProprio = false,
                Valor1Hora = 0,
                Valor2horas = 0,
                Valor30min = 0,
                ValorPernoite = 0
            };



            _clienteRepository.Add(cliente);
            await _clienteRepository.SaveChangesAsync();


        }


        public async Task Atualizar(ClienteViewModel clienteViewModel)
        {
            var cliente = _mapper.Map<Cliente>(clienteViewModel);
            var ValidationResult = new UpdateClienteCommandValidation().Validate(cliente);
            if (!ValidationResult.IsValid)
            {
                NotificarValidacoesErro(ValidationResult);
                return ;
            }
            _clienteRepository.Update(cliente);
            await _clienteRepository.SaveChangesAsync();
            

        }

       

      
        

        public void Dispose()
        {
            _clienteRepository.Dispose();
        }

        protected void NotificarValidacoesErro(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _notifications.Handle(new DomainNotification(error.PropertyName, error.ErrorMessage),new System.Threading.CancellationToken());
            }
        }

        public async Task<ClienteViewModel> ObterPorIdAsync(Guid id)
        {
            return _mapper.Map<ClienteViewModel>(await _clienteRepository.GetByIdAsync(id));
        }

        public async Task<List<ContatoViewModel>> ContatosDoClienteLogadoAsync()
        {
            var contatos = await _clienteRepository.GetContatosAsync(_user.GetUserId());
            return  _mapper.Map<List<ContatoViewModel>>(contatos);
        }

        public Task AddFotoDoClienteLogadoAsync(Guid Id, string Descricao, Stream stream)
        {
            return AddFotoAsync(_user.GetUserId(), Id, Descricao, stream);
        }

        public async  Task AddFotoAsync(Guid clienteId, Guid Id, string Descricao, Stream stream)
        {
            await _imageStorage.UploadAsync(Id, stream);
            
            if(!_notifications.HasNotifications())
            {
                var foto = new Foto() {
                    Id = Id,
                    ClienteId = clienteId,
                    Descricao = Descricao,
                    Ordem = 0,
                     StatusAnalise = EnumStatusAnalise.NaoVerificado
                };

                _clienteRepository.Add(foto);
                await _clienteRepository.SaveChangesAsync();

            }


        }

        public async Task<List<ClienteBuscaViewModel>> ClientesProximosClienteLogadoAsync( int Skip = 0, int Take = 12, string query = "")
        {
            var clientes =await _clienteRepository.GetClientesProximosAsync(_user.GetUserId(),  Skip, Take, query);
            return _mapper.Map<List<ClienteBuscaViewModel>>(clientes);
        }

        public Task AtualizarPosicaoClienteLogadoAsync(PositionVM position)
        {
            //não precisa de savechanges
            return _clienteRepository.AtualizarPosicaoAsync(_user.GetUserId(), position.Latitude, position.Longitude);
        }

        public async Task<PreferenciaVM> ObterPreferenciasAsync(Guid Id)
        {
            return _mapper.Map<PreferenciaVM>(await _clienteRepository.GetPreferenciaAsync(Id));
        }

        public async Task AtualizaPreferenciasAsync(PreferenciaVM model)
        {
            //var preferencia = _mapper.Map<Preferencia>(model);
            //_clienteRepository.Update(preferencia);
            //var count = _clienteRepository.SaveChangesAsync();


            var preferencia = await _clienteRepository.GetPreferenciaAsync(model.Id);
            //if (preferencia == null)
            //{
            //    preferencia = new Preferencia() {PrefereGeneros = new List<PreferenciaGenero>() };
            //    //_clienteRepository.db

            //}

            preferencia.Distancia = model.Distancia;
            preferencia.IdadeMinima = model.IdadeMinima;
            preferencia.IdadeMaxima = model.IdadeMaxima;
            preferencia.PrecoMaximo = model.PrecoMaximo;
            preferencia.PrecoMinimo = model.PrecoMinimo;
             
            

            if (model.Mulher && !preferencia.PrefereGeneros.Any(q => q.Genero == EnumGenero.Mulher))
            {
                preferencia.PrefereGeneros.Add(new PreferenciaGenero() { Genero = EnumGenero.Mulher, PreferenciaId = preferencia.Id });

            }
            if (!model.Mulher && preferencia.PrefereGeneros.Any(q => q.Genero == EnumGenero.Mulher))
            {
                var r = preferencia.PrefereGeneros.Single(q => q.Genero == EnumGenero.Mulher);
                preferencia.PrefereGeneros.Remove(r);
            }

            if (model.Homem && !preferencia.PrefereGeneros.Any(q => q.Genero == EnumGenero.Homem))
            {
                preferencia.PrefereGeneros.Add(new PreferenciaGenero() { Genero = EnumGenero.Homem, PreferenciaId = preferencia.Id });

            }
            if (!model.Homem && preferencia.PrefereGeneros.Any(q => q.Genero == EnumGenero.Homem))
            {
                var r = preferencia.PrefereGeneros.Single(q => q.Genero == EnumGenero.Homem);
                preferencia.PrefereGeneros.Remove(r);
            }

            if (model.Casal && !preferencia.PrefereGeneros.Any(q => q.Genero == EnumGenero.Casal))
            {
                preferencia.PrefereGeneros.Add(new PreferenciaGenero() { Genero = EnumGenero.Casal, PreferenciaId = preferencia.Id });

            }
            if (!model.Casal && preferencia.PrefereGeneros.Any(q => q.Genero == EnumGenero.Casal))
            {
                var r = preferencia.PrefereGeneros.Single(q => q.Genero == EnumGenero.Casal);
                preferencia.PrefereGeneros.Remove(r);
            }

            if (model.Trans && !preferencia.PrefereGeneros.Any(q => q.Genero == EnumGenero.Trans))
            {
                preferencia.PrefereGeneros.Add(new PreferenciaGenero() { Genero = EnumGenero.Trans, PreferenciaId = preferencia.Id });

            }
            if (!model.Trans && preferencia.PrefereGeneros.Any(q => q.Genero == EnumGenero.Trans))
            {
                var r = preferencia.PrefereGeneros.Single(q => q.Genero == EnumGenero.Trans);
                preferencia.PrefereGeneros.Remove(r);
            }


            await _clienteRepository.SaveChangesAsync();
        }

        public async Task Excluir(Guid id)
        {
             _clienteRepository.Remove(id);
            await _clienteRepository.SaveChangesAsync();
            

        }

        public async Task<Tuple<List<ClienteViewModel>, int>> Listar(int Skip = 0, int Take = 12, string query = "", string Sort = "nome", string Direction = "asc")
        {
            var resp =await _clienteRepository.Listar(Skip, Take, query, Sort, Direction);
            return new Tuple<List<ClienteViewModel>, int>(_mapper.Map<List<ClienteViewModel>>(resp.Item1), resp.Item2);
        }

        public async Task<PerfilVM> ObterPerfilAsync(Guid id)
        {
            var perfil = await _clienteRepository.GetPerfilAsync(id, _user.GetUserId());

            
            Random rnd = new Random();
            //ofusca local em 111.32 m
            perfil.Latitude += rnd.Next(0, 2) == 0 ? 0.001 : -0.001;
            //ofusca local em 111.32 m
            perfil.Longitude += rnd.Next(0, 2) == 0 ? 0.001 : -0.001;


            return _mapper.Map<PerfilVM>(perfil);
        }
    }
}
