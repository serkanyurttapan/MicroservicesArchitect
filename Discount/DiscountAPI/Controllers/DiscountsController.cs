using DiscountAPI.Models;
using DiscountAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ControllerBases;
using Shared.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscountAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;
        public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _discountService.GetAll();
            return CreateActionResult(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discount = await _discountService.GetById(id);
            return CreateActionResult(discount);
        }
        [HttpGet]
        [Route("/api/[controller]/[action]/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId = _sharedIdentityService.GetUserId();
            var discount = await _discountService.GetByCodeAndUserId(code, userId);
            return CreateActionResult(discount);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Discount discount)
        {
            return CreateActionResult(await _discountService.Save(discount));
        }
        [HttpPut]
        public async Task<IActionResult> Update(Discount discount)
        {
            return CreateActionResult(await _discountService.Update(discount));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _discountService.Delete(id));
        }
    }
}
