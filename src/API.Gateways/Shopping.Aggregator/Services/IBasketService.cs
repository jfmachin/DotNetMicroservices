using Shopping.Aggregator.Models.DTOs;

namespace Shopping.Aggregator.Services {
    public interface IBasketService {
        Task<BasketDTO> GetBasket(string userName);
    }
}
