namespace Shopping.Aggregator.Models.DTOs {
    public class BasketDTO {
        public string UserName { get; set; }
        public List<BasketItemExtendedDTO> Items { get; set; } = new List<BasketItemExtendedDTO>();
        public decimal TotalPrice { get; set; }
        
    }
}
