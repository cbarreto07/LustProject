using AutoMapper;
using Lust.App.Server.ViewModels;
using Lust.Domain.Clientes;

namespace Lust.App.Server.AutoMapper
{
    internal class ViewModelToDomainMappingContato : Profile
    {
        public ViewModelToDomainMappingContato()
        {
            CreateMap<ContatoViewModel, Cliente>();
        }
    }
}