using AutoMapper;
using Lust.App.Server.ViewModels;

using Lust.Domain.Clientes;
using Lust.Domain.Query;
using Lust.Infra.Files.Image;

namespace Lust.App.Server.AutoMapper
{
    public class DomainToViewModelMappingClienteBusca : Profile
    {

        public DomainToViewModelMappingClienteBusca()
        {
            CreateMap<ClienteQuery, ClienteBuscaViewModel>()
                .ForMember(d => d.FotoDePerfilThumbnail, opt => opt.ResolveUsing<PropertyThumb>());
        }


        class PropertyThumb : IValueResolver<ClienteQuery, ClienteBuscaViewModel, string>
        {
            private readonly IImageStorage _imageStorage;

            public PropertyThumb(IImageStorage imageStorage)
            {
                _imageStorage = imageStorage;
            }

            public string Resolve(ClienteQuery source, ClienteBuscaViewModel destination, string destMember, ResolutionContext context)
            {
                return source.FotoDePerfil == null ? "" : _imageStorage.GetCroppedUri(source.FotoDePerfil.Value, EnumProportion.p4x3, EnumSize.thumbnail).GetAwaiter().GetResult().AbsoluteUri;
            }


        }
    }

    
}
