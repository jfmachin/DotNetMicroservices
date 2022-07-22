namespace Shopping.Aggregator.Models.DTOs {
    public class ShopppingDTO {
        public string UserName { get; set; }
        public BasketDTO BasketWithProducts { get; set; }
        public IEnumerable<OrderResponseDTO> Orders { get; set; }
    }
}
