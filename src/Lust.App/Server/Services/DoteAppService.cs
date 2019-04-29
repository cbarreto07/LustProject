using AutoMapper;
using Lust.App.Server.Interfaces;
using Lust.App.Server.ViewModels;
using Lust.Domain.Clientes;
using Lust.Domain.Clientes.Interfaces;
using Lust.Domain.Clientes.Validations;
using Lust.Domain.Core.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.Services
{
    public class DoteAppService : BaseAppService, IDoteAppService
    {
        private readonly IMapper _mapper;
        private readonly IDoteRepository _doteRepository;
        

        public DoteAppService(IMapper mapper, IDoteRepository doteRepository, INotificationHandler<DomainNotification> notifications):base(notifications)
        {
            _mapper = mapper;
            _doteRepository = doteRepository;            
        }

        public async Task<Tuple<List<DoteListaVM>, int>> ListarAsync(int skip, int take, string query, string sort, string direction)
        {
            var resp = await _doteRepository.ListarAsync(skip, take, query, sort, direction);
            return new Tuple<List<DoteListaVM>, int>(_mapper.Map<List<DoteListaVM>>(resp.Item1), resp.Item2);
        }

        public async Task<DoteVM> ObterPorIdAsync(Guid id)
        {
            return _mapper.Map<DoteVM>(await _doteRepository.GetByIdAsync(id));
        }

        public async Task<DoteVM> Atualizar(DoteVM model)
        {
            var entity = _mapper.Map<Dote>(model);
            var ValidationResult = new UpdateDoteCommandValidation().Validate(entity);
            if (!ValidationResult.IsValid)
            {
                await NotificarValidacoesErro(ValidationResult);
                return model;
            }
            _doteRepository.Update(entity);
            await _doteRepository.SaveChangesAsync();
            return _mapper.Map<DoteVM>(entity); ;
        }

        public async Task<DoteVM> Criar(DoteVM model)
        {
            var entity = _mapper.Map<Dote>(model);
            var ValidationResult = new CreateDoteCommandValidation().Validate(entity);
            if (!ValidationResult.IsValid)
            {
                await NotificarValidacoesErro(ValidationResult);
                return model;
            }
            _doteRepository.Add(entity);
            await _doteRepository.SaveChangesAsync();

            return _mapper.Map<DoteVM>(entity);
        }

        public async Task Excluir(Guid id)
        {
            var entity = await _doteRepository.GetByIdAsync(id);
            if (entity == null)
            {
                await NotificarErro("Id", "Não localizado");
                return;
            }

            _doteRepository.Remove(entity);

            try
            {                
                await _doteRepository.SaveChangesAsync();
                return;
            }
            catch
            {
                await NotificarErro("Dote", "Não é possível excluir");
                return;
            }


        }



        
    }
}
