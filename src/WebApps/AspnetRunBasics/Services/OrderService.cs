﻿using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services { 
    public class OrderService : IOrderService {
        private readonly HttpClient httpClient;

        public OrderService(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName) {
            var response = await httpClient.GetAsync($"/order/{userName}");
            return await response.ReadContentAs<List<OrderResponseModel>>();
        }
    }
}
