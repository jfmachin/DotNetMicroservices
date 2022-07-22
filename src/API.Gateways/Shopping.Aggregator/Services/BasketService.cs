using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models.DTOs;

namespace Shopping.Aggregator.Services {
    public class BasketService : IBasketService {
        private readonly HttpClient client;

        public BasketService(HttpClient client) {
            this.client = client;
        }
        public async Task<BasketDTO> GetBasket(string userName) {
            var basket = await client.GetAsync($"/api/v1/basket/{userName}");
            return await basket.ReadContentAs<BasketDTO>();
        }
    }
}
