﻿using BasketAPI.Dtos;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace BasketAPI.Services
{

    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;
        public BasketService(RedisService redisService = null)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> Delete(string userId)
        {
            var status = await _redisService.GetDB().KeyDeleteAsync(userId);
            return status ? Response<bool>.Success(204) : Response<bool>.Fail("basket not found", 404);

        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            var existBasket = await _redisService.GetDB()
                                                 .StringGetAsync(userId);
            if (string.IsNullOrEmpty(existBasket))
            {
                return Response<BasketDto>.Fail("basketNotFound", 404);
            }
            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDB()
                                            .StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
            return status ? Response<bool>.Success(204) : Response<bool>.Fail("basket could not update or save", 500);
        }
    }
}
