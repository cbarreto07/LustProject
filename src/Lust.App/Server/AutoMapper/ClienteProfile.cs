using AutoMapper;
using Lust.App.Server.ViewModels;
using Lust.Domain.Clientes;
using Lust.Domain.Query;
using Lust.Infra.Files.Image;

namespace Lust.App.Server.AutoMapper
{
    public  class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            //CreateMap<PerfilVM, PerfilQuery>();
            CreateMap<PerfilQuery, PerfilVM>()
                .ForMember(d => d.FotoDePerfilThumbnail, opt => opt.ResolveUsing<PropertyThumb>())
                .ForMember(d => d.FotoDeCapaThumbnail, opt => opt.ResolveUsing<PropertyCapa>());

            CreateMap<FotoQuery, FotoPerfilVM>()
                .ForMember(d => d.Thumbnail, opt => opt.ResolveUsing<PropertyFotoThumb>())
                .ForMember(d => d.Thumbnail_1x1, opt => opt.ResolveUsing<PropertyFoto_1x1Thumb>())
                ;



            CreateMap<DoteQuery, DotePerfilVM>();

        }


        class PropertyThumb : IValueResolver<PerfilQuery, PerfilVM, string>
        {
            private readonly IImageStorage _imageStorage;

            public PropertyThumb(IImageStorage imageStorage)
            {
                _imageStorage = imageStorage;
            }

            public string Resolve(PerfilQuery source, PerfilVM destination, string destMember, ResolutionContext context)
            {
                return source.FotoDePerfil == null ? "" : _imageStorage.GetCroppedUri(source.FotoDePerfil.Value, EnumProportion.p1x1, EnumSize.thumbnail).GetAwaiter().GetResult().AbsoluteUri;
            }


        }

        class PropertyCapa : IValueResolver<PerfilQuery, PerfilVM, string>
        {
            private readonly IImageStorage _imageStorage;

            public PropertyCapa(IImageStorage imageStorage)
            {
                _imageStorage = imageStorage;
            }

            public string Resolve(PerfilQuery source, PerfilVM destination, string destMember, ResolutionContext context)
            {
                return source.FotoDeCapa == null ? "" : _imageStorage.GetCroppedUri(source.FotoDeCapa.Value, EnumProportion.p16x9, EnumSize.lg).GetAwaiter().GetResult().AbsoluteUri;
            }
        }

        class PropertyFoto_1x1Thumb : IValueResolver<FotoQuery, FotoPerfilVM, string>
        {
            private readonly IImageStorage _imageStorage;

            public PropertyFoto_1x1Thumb(IImageStorage imageStorage)
            {
                _imageStorage = imageStorage;
            }

            public string Resolve(FotoQuery source, FotoPerfilVM destination, string destMember, ResolutionContext context)
            {
                return  _imageStorage.GetCroppedUri(source.Id, EnumProportion.p1x1, EnumSize.thumbnail).GetAwaiter().GetResult().AbsoluteUri;
            }
        }

        class PropertyFotoThumb : IValueResolver<FotoQuery, FotoPerfilVM, string>
        {
            private readonly IImageStorage _imageStorage;

            public PropertyFotoThumb(IImageStorage imageStorage)
            {
                _imageStorage = imageStorage;
            }

            public string Resolve(FotoQuery source, FotoPerfilVM destination, string destMember, ResolutionContext context)
            {
                return _imageStorage.GetResizedUri(source.Id, EnumSize.thumbnail).GetAwaiter().GetResult().AbsoluteUri;
            }
        }




    }
}