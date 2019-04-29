using AutoMapper;
using Lust.App.Server.ViewModels;
using Lust.Domain.Clientes;
using System.Linq;



namespace Lust.App.Server.AutoMapper
{
    internal class ViewModelToDomainMappingCaracteristica : Profile
    {
        public ViewModelToDomainMappingCaracteristica()
        {
            CreateMap<CaracteristicaVM, Caracteristica>()
                .ForMember(dest => dest.AtendeGeneros, opt => opt.Ignore());
        }
    }
}