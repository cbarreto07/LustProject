using System;
using AutoMapper;
using Lust.App.Server.ViewModels;
using Lust.Domain.Clientes;

namespace Lust.App.Server.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ClienteViewModel, Cliente>();
               
        }
    }
}