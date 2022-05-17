using Dapper;
using DiscountAPI.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DiscountAPI.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;
        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }
        public async Task<Response<NoContent>> Delete(int id)
        {
            int deleteStatus = await _dbConnection.ExecuteAsync("DELETE FROM discount WHERE id=@Id", new { id });

            return deleteStatus > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("discount not found", 404);
        }

        public async Task<Response<List<Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Discount>("SELECT * FROM discount");
            return Response<List<Discount>>.Success(discounts.ToList(), 200);
            ;
        }

        public async Task<Response<Discount>> GetByCodeAndUserId(string code, string userId)
        {

            var discount = await _dbConnection.QueryAsync<Discount>("SELECT * FROM discount where userid=@UserId,code=@Code", new { userId, code });
            var hasDiscount = discount.FirstOrDefault();
            return hasDiscount != null ? Response<Discount>.Success(hasDiscount, 200) : Response<Discount>.Fail("discount not found", 404);
        }

        public async Task<Response<Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Discount>("SELECT * FROM discount WHERE id =@Id", new { Id = id })).SingleOrDefault();
            return discount is null ? Response<Discount>.Fail("discount not found", 404)
                                    : Response<Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Discount discount)
        {
            int saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount(userid,rate,code) VALUES (@UserId,@Rate,@Code)", discount);
            return saveStatus > 0 ? Response<NoContent>.Success(200) : Response<NoContent>.Fail("an error occurred while adding", 500);
        }

        public async Task<Response<NoContent>> Update(Discount discount)
        {

            int updateStatus = await _dbConnection.ExecuteAsync("UPDATE discount SET userid=@UserId,rate=@Rate,code=@Code WHERE id=@Id",
                new
                {
                    discount.UserId,
                    discount.Rate,
                    discount.Code,
                    discount.Id
                });
            return updateStatus > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("discount not found", 404);
        }
    }
}
