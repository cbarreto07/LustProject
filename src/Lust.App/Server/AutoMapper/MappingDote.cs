using AutoMapper;
using Lust.App.Server.ViewModels;
using Lust.Domain.Clientes;
using Lust.Domain.Query;

namespace Lust.App.Server.AutoMapper
{
    internal class MappingDote : Profile
    {
        public MappingDote()
        {

            CreateMap<Dote, DoteVM>();
            CreateMap<DoteQuery, DoteListaVM>();
            CreateMap<DoteVM, Dote>();
            
        }
    }
}