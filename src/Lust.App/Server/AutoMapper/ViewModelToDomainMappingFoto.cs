using AutoMapper;
using Lust.App.Server.ViewModels;
using Lust.Domain.Clientes;

namespace Lust.App.Server.AutoMapper
{
    internal class ViewModelToDomainMappingFoto : Profile
    {
        public ViewModelToDomainMappingFoto()
        {
            CreateMap< FotoVM, Foto>();
        }
    }
}