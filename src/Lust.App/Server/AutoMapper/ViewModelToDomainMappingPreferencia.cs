using AutoMapper;
using Lust.App.Server.ViewModels;
using Lust.Domain.Clientes;
using System.Linq;


namespace Lust.App.Server.AutoMapper
{
    public  class ViewModelToDomainMappingPreferencia : Profile
    {
        public ViewModelToDomainMappingPreferencia()
        {
            CreateMap<PreferenciaVM, Preferencia>()
                .ForMember(dest => dest.PrefereGeneros, opt => opt.Ignore());
        }

        
    }

}