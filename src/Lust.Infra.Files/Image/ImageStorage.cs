using Lust.Infra.Files.Storage;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.Shapes;
using SixLabors.ImageSharp.Processing.Transforms;
using System.Linq;
using MediatR;
using Lust.Domain.Core.Notifications;

namespace Lust.Infra.Files.Image
{
    public class ImageStorage : IImageStorage
    {
        private enum Tipo
        {
            original,
            resized,
            cropped
        }
        private  int[] dimensoes =  { 160,360,640,800,1280,1600,1920 };

        private ImageLinkCache _linkCache;
        private readonly IAzureBlobStorage _azureBlobStorage;
        private readonly INotificationHandler<DomainNotification> _notifications;

        public ImageStorage(IAzureBlobStorage azureBlobStorage, INotificationHandler<DomainNotification> notifications, ImageLinkCache linkCache)
        {
            _azureBlobStorage = azureBlobStorage;
            _notifications = notifications;
            _linkCache = linkCache;
        }

        public async Task<Uri> GetCroppedUri(Guid name, int withSize, int heightSize, Stream originalStream = null)
        {
            var nameInStorage = GetNameInStorage(name, Tipo.cropped, withSize, heightSize);
            if (originalStream == null)
            {
                if (_linkCache.Cache.ContainsKey(nameInStorage))
                    return _linkCache.Cache[nameInStorage];

                if (await _azureBlobStorage.ExistsAsync(nameInStorage))
                {
                    var uri = GetTrueUri(nameInStorage);
                    _linkCache.Cache.Add(nameInStorage, uri);
                    return uri;
                }

                var originalNameInStorge = GetNameInStorage(name, Tipo.original, 0, 0);//obtem a uri da imagem  original para gerar a cropped

                if (!await _azureBlobStorage.ExistsAsync(originalNameInStorge))
                {
                    await _notifications.Handle(new DomainNotification("Id", "Imagem não localizada"), new System.Threading.CancellationToken());
                    return GetErroUri();
                }

                originalStream = await _azureBlobStorage.GetStreamAsync(originalNameInStorge);
            }
            originalStream.Seek(0, SeekOrigin.Begin);
            using (Image<Rgba32> image = SixLabors.ImageSharp.Image.Load(originalStream))
            {
                
                
                //verifica se da pra fazer o crop 
                if (withSize < image.Width && heightSize < image.Height)
                {
                    Image<Rgba32> result = image.Clone(ctx => ctx.Resize(
                            new ResizeOptions
                            {
                                Size = new Size(withSize, heightSize),
                                Mode = ResizeMode.Crop
                            }));
                    var resultStream = new MemoryStream();
                    result.SaveAsPng(resultStream);

                    await _azureBlobStorage.UploadAsync(nameInStorage, resultStream);
                    var uri = GetTrueUri(nameInStorage);
                    _linkCache.Cache.Add(nameInStorage, uri);
                    return uri;
                }
                else // se não der pra fazer pega dimensões abaixo das solicitadas
                {
                    
                    double fator = 1;
                    fator = withSize - image.Width > heightSize - image.Height ?   (double)image.Width / (double)withSize :  (double)image.Height / (double)heightSize;
                    var novoWidth = (int)Math.Floor( withSize * fator);

                    if(dimensoes.Where(q => q < novoWidth).Any())
                    {
                        novoWidth = dimensoes.Where(q => q < novoWidth).Max();
                    }
                    var novoHeight = (int)Math.Round(((double)novoWidth / (double)withSize) * heightSize); // para manter a proporção solicitada

                    if (novoWidth != image.Width && novoHeight != image.Height)
                    {
                        var novoUri = await GetCroppedUri(name, novoWidth, novoHeight);

                        _linkCache.Cache.Add(nameInStorage, novoUri);
                        return novoUri;
                    }

                    //retorna o original se estiver menor q o solicitado e nas proporsoes solicitadas
                    var originalNameInStorge = GetNameInStorage(name, Tipo.original, 0, 0);//obtem a uri da imagem  original para gerar a cropped
                    var uri = GetTrueUri(originalNameInStorge);
                    _linkCache.Cache.Add(nameInStorage, uri);
                    return uri;




                }

                
            }


            
        }

        public Task<Uri> GetCroppedUri(Guid name, EnumProportion Proportion, EnumSize size)
        {
            var w = (int)size;
            var h = 0;
            switch (Proportion)
            {
                case EnumProportion.p1x1:
                    h = w;
                    break;
                case EnumProportion.P3x4:
                    h = 4* w / 3;
                    break;
                case EnumProportion.p4x3:
                    h = 3 * w / 4;
                    break;
                case EnumProportion.p16x9:
                    h = 9 * w / 16;
                    break;
                case EnumProportion.p9x16:
                    h = 16 * w / 9;
                    break;
            }
            return GetCroppedUri(name, w, h);
        }

        public Task<Uri> GetResizedUri(Guid name, EnumSize size)
        {
            return GetResizedUri(name, (int)size);
        }

        public async Task<Uri> GetResizedUri(Guid name, int withSize,Stream originalStream = null) // só faz o resize pela largura da imagem
        {
            var nameInStorage = GetNameInStorage(name, Tipo.resized, withSize, 0);//obtem a uri da imagem
            if (originalStream == null)
            {
                if (_linkCache.Cache.ContainsKey(nameInStorage)) // já está verificado
                    return _linkCache.Cache[nameInStorage];

                if (await _azureBlobStorage.ExistsAsync(nameInStorage))
                {
                    var uri = GetTrueUri(nameInStorage);
                    _linkCache.Cache.Add(nameInStorage, uri);
                    return uri;
                }

                var originalNameInStorge = GetNameInStorage(name, Tipo.original, 0, 0);//obtem a uri da imagem  original para gerar a cropped

                if (!await _azureBlobStorage.ExistsAsync(originalNameInStorge))
                {
                    await _notifications.Handle(new DomainNotification("Id", "Imagem não localizada"), new System.Threading.CancellationToken());
                    return GetErroUri();
                }

                originalStream = await _azureBlobStorage.GetStreamAsync(originalNameInStorge);
            }
            originalStream.Seek(0, SeekOrigin.Begin);
            using (Image<Rgba32> image = SixLabors.ImageSharp.Image.Load(originalStream))
            {

                if (withSize < image.Width )
                {
                    var heightSize = (withSize * image.Height) / image.Width;
                    Image<Rgba32> result = image.Clone(ctx => ctx.Resize(
                            new ResizeOptions
                            {
                                Size = new Size(withSize, heightSize),
                                Mode = ResizeMode.Crop
                            }));
                    var resultStream = new MemoryStream();
                    result.SaveAsPng(resultStream);

                    await _azureBlobStorage.UploadAsync(nameInStorage, resultStream);
                    _linkCache.Cache.Add(nameInStorage, GetTrueUri(nameInStorage));
                    return GetTrueUri(nameInStorage);
                }
                else
                {
                    

                    if (dimensoes.Where(q => q < withSize).Any()) // pega a dimensao padrao imediatamente inferior a solicitada
                    {
                        withSize = dimensoes.Where(q => q < withSize).Max();
                        var novoUri = await GetResizedUri(name, withSize);
                        _linkCache.Cache.Add(nameInStorage, novoUri);
                        return novoUri;
                    }

                    //retorna a original se ela for muito pequena
                    var originalNameInStorge = GetNameInStorage(name, Tipo.original, 0, 0);//obtem a uri da imagem  original para gerar a cropped
                    var uri = GetTrueUri(originalNameInStorge);
                    _linkCache.Cache.Add(nameInStorage, uri);
                    return uri;



                }





            }



            
        }

        public async Task<Uri> GetUri(Guid name) // imagem original
        {
            var nameInStorage = GetNameInStorage(name, Tipo.original, 0, 0);//obtem a uri da imagem
            if (_linkCache.Cache.ContainsKey(nameInStorage)) // já está verificado
                return _linkCache.Cache[nameInStorage];
            

            if (!await _azureBlobStorage.ExistsAsync(nameInStorage))
            {
                await _notifications.Handle(new DomainNotification("Id", "Imagem não localizada"), new System.Threading.CancellationToken());
                return GetErroUri();
            }

            var uri = GetTrueUri(nameInStorage);
            _linkCache.Cache.Add(nameInStorage, uri);
            return uri;
        }

        public async Task UploadAsync(Guid name, Stream stream)
        {

            //var uri = GetTesteUri(name, Tipo.original,0,0);
            var nameInStorage = GetNameInStorage(name, Tipo.original, 0, 0);
            stream.Seek(0, SeekOrigin.Begin);
            using (Image<Rgba32> image = SixLabors.ImageSharp.Image.Load(stream))
            {
            
                var resultStream = new MemoryStream();
                image.SaveAsPng(resultStream);
                await _azureBlobStorage.UploadAsync(nameInStorage, resultStream);
                _linkCache.Cache.Add(nameInStorage, GetTrueUri(nameInStorage));
            

            }
            //gera os thumbs
            var w = (int)EnumSize.thumbnail;
            await GetCroppedUri(name, w, w , stream);
            await GetCroppedUri(name, w, 4 * w / 3, stream);
            await GetCroppedUri(name, w, 3 * w / 4, stream);
            await GetCroppedUri(name, w, 9 * w / 16, stream);
            await GetCroppedUri(name, w, 16 * w / 9, stream);
            await GetResizedUri(name, w, stream);
        }

        private Uri GetErroUri()
        {
            return new Uri(_azureBlobStorage.StorageUri(), "erros/naolocalizado.png"); ;
        }

        private Uri GetTrueUri(string nameInStorage)
        {
          
            return new Uri(_azureBlobStorage.StorageUri(), nameInStorage); ;
        }

        private string GetNameInStorage(Guid guid, Tipo tipo, int with, int height)
        {
            
            string name = "";
            switch (tipo)
            {
                case Tipo.original:
                    //uri = new Uri(uri, "original");
                    name = "o/";
                    break;
                case Tipo.resized:
                    //uri = new Uri(uri, "resized");
                    //uri = new Uri(uri, with.ToString());      
                    name = $"r/{with}/";
                    break;
                case Tipo.cropped:
                    //uri = new Uri(uri, "cropped");
                    //uri = new Uri(uri, with.ToString());
                    //uri = new Uri(uri, height.ToString());                    
                    name = $"c/{with}/{height}/";
                    break;
            }
            name += guid.ToString() + ".png";


            return name;
        }
    }
}
