using AutoMapper;
using Lust.App.Server.ViewModels;
using Lust.Domain.Clientes;

namespace Lust.App.Server.AutoMapper
{
    internal class DomainToViewModelMappingContato : Profile
    {
        public DomainToViewModelMappingContato()
        {
            CreateMap<Cliente, ContatoViewModel>();
        }
    }
}