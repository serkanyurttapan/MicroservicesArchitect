﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWeb.Models.Catalog;
using MVCWeb.Services.Interfaces;
using Shared.Service;
using System.Threading.Tasks;

namespace MVCWeb.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;
        public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId()));
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput courseCreateInput)
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View();
            }

            courseCreateInput.UserId = _sharedIdentityService.GetUserId();
            await _catalogService.AddCourseAsync(courseCreateInput);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(string Id)
        {
            CourseUpdateInput courseUpdateInput = null;
            var course = await _catalogService.GetByCourseIdAsync(Id);
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", course.Id);

            if (course is null)
                RedirectToAction(nameof(Index));//show messagge 

            else
            {
                courseUpdateInput = new()
                {
                    Id = course.Id,
                    Name = course.Name,
                    Price = course.Price,
                    Feature = course.Feature,
                    CategoryId = course.CategoryId,
                    UserId = course.UserId,
                    Picture = course.Picture
                };
                await _catalogService.UpdateCourseAsync(courseUpdateInput);
            }
            return View(courseUpdateInput);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInput courseUpdateInput)
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", courseUpdateInput.Id);
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _catalogService.UpdateCourseAsync(courseUpdateInput);
           return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            await _catalogService.DeleteCourseAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
