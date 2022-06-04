using Microsoft.AspNetCore.Http;
using MVCWeb.Models.PhotoStocks;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWeb.Services.Interfaces
{
    public interface IPhotoStockService
    {
        Task<Response<PhotoViewModel>> UploadPhoto(IFormFile formFile);
        Task<bool> DeletePhoto(string url);
    }
}
