﻿using Basket.API.Models.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories {
    public class BasketRepository : IBasketRepository {
        private readonly IDistributedCache redisCache;

        public BasketRepository(IDistributedCache redisCache) {
            this.redisCache = redisCache;
        }
        public async Task DeleteBasket(string userName) {
            await redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasket(string userName) {
            var basket = await redisCache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(basket))
                return null;
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket) {
            await redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }
    }
}
