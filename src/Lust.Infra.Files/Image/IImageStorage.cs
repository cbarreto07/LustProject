using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lust.Infra.Files.Image
{
    public interface  IImageStorage
    {
        Task UploadAsync(Guid name, Stream stream);
        Task<Uri> GetUri(Guid name);
        Task<Uri> GetResizedUri(Guid name, EnumSize size);
        Task<Uri> GetResizedUri(Guid name, int withSize, Stream originalStream = null);
        Task<Uri> GetCroppedUri(Guid name, int withSize, int heightSize, Stream originalStream = null);
        Task<Uri> GetCroppedUri(Guid name, EnumProportion Proportion, EnumSize size);
    }
}
