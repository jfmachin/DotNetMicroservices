using Shopping.Aggregator.Models.DTOs;

namespace Shopping.Aggregator.Services {
    public interface IOrderService {
        Task<IEnumerable<OrderResponseDTO>> GetOrderByUserName(string userName);
    }
}
