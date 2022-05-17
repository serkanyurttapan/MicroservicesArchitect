using DiscountAPI.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscountAPI.Services
{
    public interface IDiscountService
    {
        Task<Response<List<Discount>>> GetAll();
        Task<Response<Discount>> GetById(int Id);
        Task<Response<NoContent>> Save(Discount discount);
        Task<Response<NoContent>> Update(Discount discount);
        Task<Response<NoContent>> Delete(int Id);
        Task<Response<Discount>> GetByCodeAndUserId(string code, string userId);
    }
}
