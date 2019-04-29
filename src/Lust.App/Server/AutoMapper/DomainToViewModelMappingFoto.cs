using AutoMapper;
using Lust.App.Server.ViewModels;
using Lust.Domain.Clientes;

namespace Lust.App.Server.AutoMapper
{
    internal class DomainToViewModelMappingFoto : Profile
    {
        public DomainToViewModelMappingFoto()
        {
            CreateMap<Foto, FotoVM>();
        }
    }
}