﻿using Basket.API.Models.Entities;

namespace Basket.API.Repositories {
    public interface IBasketRepository {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task DeleteBasket(string userName);
    }
}
