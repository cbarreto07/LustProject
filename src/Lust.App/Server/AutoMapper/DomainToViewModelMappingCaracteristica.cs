using AutoMapper;
using Lust.App.Server.ViewModels;
using Lust.Domain.Clientes;
using System.Linq;


namespace Lust.App.Server.AutoMapper
{
    internal class DomainToViewModelMappingCaracteristica : Profile
    {
        public DomainToViewModelMappingCaracteristica()
        {
            CreateMap<Caracteristica, CaracteristicaVM>()
                .ForMember(dest => dest.Mulher, m => m.MapFrom(src => src.AtendeGeneros.Any(q => q.Genero == EnumGenero.Mulher)))
                .ForMember(dest => dest.Homem, m => m.MapFrom(src => src.AtendeGeneros.Any(q => q.Genero == EnumGenero.Homem)))
                .ForMember(dest => dest.Casal, m => m.MapFrom(src => src.AtendeGeneros.Any(q => q.Genero == EnumGenero.Casal)))
                .ForMember(dest => dest.Trans, m => m.MapFrom(src => src.AtendeGeneros.Any(q => q.Genero == EnumGenero.Trans)));
        }
    }
}