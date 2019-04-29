using AutoMapper;
using Lust.App.Server.ViewModels;
using Lust.Domain.Clientes;

namespace Lust.App.Server.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Cliente, ClienteViewModel>();
            
        }
    }
}