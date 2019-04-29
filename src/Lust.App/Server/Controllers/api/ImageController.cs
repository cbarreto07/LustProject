using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lust.Domain.Core.Notifications;
using Lust.Domain.Interfaces;
using Lust.Infra.CrossCutting.AspNetFilters;
using Lust.Infra.Files.Image;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lust.App.Server.Controllers.api
{
    [Produces("application/json")]
    [Route("api/Image")]
    public class ImageController : BaseController
    {
        private readonly IImageStorage _imageStorage;
        public ImageController(
                    INotificationHandler<DomainNotification> notifications,
                    IUser user,
                    IImageStorage imageStorage) : base(notifications, user)
        {
            _imageStorage = imageStorage;

        }

        [HttpGet("{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get( Guid Id)
        {
            var uri = await _imageStorage.GetUri(Id);
            

            return Response(uri.AbsoluteUri);
        }

        [HttpGet("{Width}/{Height}/{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Crop(int Width,int  Height, Guid Id)
        {
            var uri  = await _imageStorage.GetCroppedUri(Id, Width, Height);

            return Response(uri.AbsoluteUri);
        }

        [HttpGet("{Proportion}/{Size}/{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Crop(EnumProportion Proportion, EnumSize Size, Guid Id)
        {
            var uri = await _imageStorage.GetCroppedUri(Id, Proportion, Size);

            return Response(uri.AbsoluteUri);
        }

        [HttpGet("{Width}/{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Resize(int Width,  Guid Id)
        {
            var uri = await _imageStorage.GetResizedUri(Id, Width);

            return Response(uri.AbsoluteUri);
        }

        [HttpGet("{Size}/{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Resize( EnumSize Size, Guid Id)
        {
            var uri = await _imageStorage.GetResizedUri(Id, Size);

            return Response(uri.AbsoluteUri);
        }


        [HttpPost]
        [AllowAnonymous]
        
        public async Task<IActionResult> Post(IFormFile file)
        {
            if (string.IsNullOrEmpty(file?.ContentType) || (file.Length == 0)) return BadRequest(new ApiError("Image provided is invalid"));

            var size = file.Length;

            if (size > Convert.ToInt64(Startup.Configuration["MaxImageUploadSize"])) return BadRequest(new ApiError("Image size greater than allowed size"));
            using (var memoryStream = new MemoryStream())
            {

                await file.CopyToAsync(memoryStream);
                await _imageStorage.UploadAsync(Guid.NewGuid(), memoryStream);

            }




                return NoContent();

        }

    }
}