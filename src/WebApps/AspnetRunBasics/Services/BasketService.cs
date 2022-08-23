using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services {
    public class BasketService : IBasketService {
        private readonly HttpClient httpClient;

        public BasketService(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public async Task<BasketModel> GetBasket(string userName) {
            var response = await httpClient.GetAsync($"/basket/{userName}");
            return await response.ReadContentAs<BasketModel>();
        }

        public async Task<BasketModel> UpdateBasket(BasketModel model) {
            var response = await httpClient.PostAsJson($"/basket", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<BasketModel>();
            else
                throw new Exception("Something went wrong when calling api");
        }

        public async Task CheckoutBasket(BasketCheckoutModel model) {
            var response = await httpClient.PostAsJson($"/basket/checkout", model);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Something went wrong when calling api");
        }
    }
}
