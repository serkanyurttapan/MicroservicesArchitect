using MVCWeb.Helper;
using MVCWeb.Models;
using MVCWeb.Models.Catalog;
using MVCWeb.Services.Interfaces;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MVCWeb.Services
{
    public class CatalogService : ICatalogService
    {
        //private readonly ClientSettings _clientSettings;
        //private readonly ServiceApiSettings _serviceApiSettings;

        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }


        public async Task<bool> AddCourseAsync(CourseCreateInput courseCreateInput)
        {

            var resultPhotoService = await _photoStockService.UploadPhoto(courseCreateInput.PhotoFormFile);
            if (resultPhotoService != null)
            {
                courseCreateInput.Picture = resultPhotoService.Data.Url;
            }

            var response = await _httpClient.PostAsJsonAsync("courses", courseCreateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/{courseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("categories");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseStatus = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();
            return responseStatus.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            //http://localhost:50000/services/catalog/courses
            var response = await _httpClient.GetAsync("courses");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            responseSuccess.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });
            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");

            if (!response.IsSuccessStatusCode)
                return null;

            var responseStatus = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            responseStatus.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

            return responseStatus.Data;
        }

        public async Task<CourseViewModel> GetByCourseIdAsync(string courseId)
        {
            var response = await _httpClient.GetAsync($"courses/{courseId}");

            if (!response.IsSuccessStatusCode)
                return null;

            var responseStatus = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            responseStatus.Data.StockPictureUrl = _photoHelper.GetPhotoStockUrl(responseStatus.Data.Picture);

            return responseStatus.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var response = await _httpClient.PutAsJsonAsync("courses", courseUpdateInput);
            return response.IsSuccessStatusCode;
        }
    }
}
