using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models.DTOs;
using Shopping.Aggregator.Services;

namespace Shopping.Aggregator.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingController: ControllerBase {
        private readonly IBasketService basketService;
        private readonly ICatalogService catalogService;
        private readonly IOrderService orderService;

        public ShoppingController(IBasketService basketService, ICatalogService catalogService, IOrderService orderService) {
            this.basketService = basketService;
            this.catalogService = catalogService;
            this.orderService = orderService;
        }

        [HttpGet("{userName}", Name = "GetShopping")]
        public async Task<ActionResult<ShopppingDTO>> getShopping(string userName) {
            var basket = await basketService.GetBasket(userName);
            foreach (var item in basket.Items) {
                var product = await catalogService.GetCatalog(item.ProductId);
                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
            }

            var orders = await orderService.GetOrderByUserName(userName);

            var shopping = new ShopppingDTO { 
                BasketWithProducts = basket, 
                Orders = orders, 
                UserName= userName
            };
            return Ok(shopping);
        }
    }
}
