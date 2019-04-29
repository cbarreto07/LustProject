using AutoMapper;
using Lust.App.Server.ViewModels;
using Lust.Domain.Clientes;
using System.Linq;

namespace Lust.App.Server.AutoMapper
{
    public class DomainToViewModelMappingPreferencia : Profile
    {
        public DomainToViewModelMappingPreferencia()
        {
            AllowNullCollections = true;
            CreateMap<Preferencia, PreferenciaVM>()
                .ForMember(dest => dest.Mulher, m => m.MapFrom(src => src.PrefereGeneros.Any(q => q.Genero == EnumGenero.Mulher)))
                .ForMember(dest => dest.Homem, m => m.MapFrom(src => src.PrefereGeneros.Any(q => q.Genero == EnumGenero.Homem)))
                .ForMember(dest => dest.Casal, m => m.MapFrom(src => src.PrefereGeneros.Any(q => q.Genero == EnumGenero.Casal)))
                .ForMember(dest => dest.Trans, m => m.MapFrom(src => src.PrefereGeneros.Any(q => q.Genero == EnumGenero.Trans)));
        }
    }
}
