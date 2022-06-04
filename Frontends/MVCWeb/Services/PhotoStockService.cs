using Microsoft.AspNetCore.Http;
using MVCWeb.Models.PhotoStocks;
using MVCWeb.Services.Interfaces;
using Newtonsoft.Json;
using Shared.Dtos;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCWeb.Services
{
    public class PhotoStockService : IPhotoStockService
    {
        private readonly HttpClient _httpClient;

        public PhotoStockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeletePhoto(string url)
        {
            var response = await _httpClient.DeleteAsync($"photos?photoUrl={url}");
            return response.IsSuccessStatusCode; 
        }

        public async Task<Response<PhotoViewModel>> UploadPhoto(IFormFile formFile)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                return null;
            }
            using var memoryStream = new MemoryStream();

            await formFile.CopyToAsync(memoryStream);

            var multipartContent = new MultipartFormDataContent();

            var randomFilename = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";

            multipartContent.Add(new ByteArrayContent(memoryStream.ToArray()), "formFile", randomFilename);

            var response = await _httpClient.PostAsync("photos", multipartContent);

            if (response.IsSuccessStatusCode)
            {
                return null;
            }
            var readResult = await response.Content.ReadAsStringAsync();

            return Response<PhotoViewModel>.Success(JsonConvert.DeserializeObject<PhotoViewModel>(readResult), 200);

        }
    }
}
