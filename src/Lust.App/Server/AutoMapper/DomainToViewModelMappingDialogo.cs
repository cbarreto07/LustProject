using AutoMapper;
using Lust.App.Server.ViewModels;
using Lust.Domain.Chats;

namespace Lust.App.Server.AutoMapper
{
    public  class DomainToViewModelMappingDialogo : Profile
    {
        public DomainToViewModelMappingDialogo()
        {
            CreateMap<Dialogo, DialogoVM>()
                .ForMember(dest => dest.Who, m => m.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.Message, m => m.MapFrom(src => src.Mensagem))
                .ForMember(dest => dest.Time, m => m.MapFrom(src => src.DataHoraCriacao));
        }
    }
}