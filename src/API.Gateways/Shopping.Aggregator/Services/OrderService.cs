using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models.DTOs;

namespace Shopping.Aggregator.Services {
    public class OrderService : IOrderService {
        private readonly HttpClient client;

        public OrderService(HttpClient client) {
            this.client = client;
        }
        public async Task<IEnumerable<OrderResponseDTO>> GetOrderByUserName(string userName) {
            var order = await client.GetAsync($"/api/v1/order/{userName}");
            return await order.ReadContentAs<List<OrderResponseDTO>>();
        }
    }
}
