using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoStockAPI.Dtos;
using Shared.ControllerBases;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoStockAPI.Controllers
{
    //blob storage azure arastir
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                return CreateActionResult(Response<PhotoDto>.Fail("photo is empty", 400));
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", formFile.FileName);
            using var stream = new FileStream(path, FileMode.Create);
            await formFile.CopyToAsync(stream, cancellationToken);
            var returnPath = formFile.FileName;

            PhotoDto photoDto = new()
            {
                Url = returnPath
            };
            return CreateActionResult(Response<PhotoDto>.Success(photoDto, 200));
        }

        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResult(Response<NoContent>.Fail("photo not found", 404));
            }
            System.IO.File.Delete(path);
            return CreateActionResult(Response<NoContent>.Success(204));
        }

    }
}
