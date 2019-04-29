using AutoMapper;

namespace Lust.App.Server.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(ps =>
            {
                ps.AddProfile(new ClienteProfile());

                ps.AddProfile(new DomainToViewModelMappingProfile());
                ps.AddProfile(new ViewModelToDomainMappingProfile());

                ps.AddProfile(new DomainToViewModelMappingContato());
                ps.AddProfile(new ViewModelToDomainMappingContato());

                ps.AddProfile(new DomainToViewModelMappingDialogo());

                ps.AddProfile(new DomainToViewModelMappingClienteBusca());

                ps.AddProfile(new DomainToViewModelMappingPreferencia());
                ps.AddProfile(new ViewModelToDomainMappingPreferencia());

                ps.AddProfile(new DomainToViewModelMappingCaracteristica());
                ps.AddProfile(new ViewModelToDomainMappingCaracteristica());

                ps.AddProfile(new DomainToViewModelMappingFoto());
                ps.AddProfile(new ViewModelToDomainMappingFoto());

                ps.AddProfile(new MappingDote());
                ps.AddProfile(new MappingPlano());

            });
        }
    }
}