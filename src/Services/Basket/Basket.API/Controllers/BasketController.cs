using AutoMapper;
using Basket.API.Models.Entities;
using Basket.API.Repositories;
using Basket.API.Services.gRPC;
using EventBus.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase {
        private readonly IPublishEndpoint publishEndpoint;
        private readonly DiscountgRPCService discountgRPC;
        private readonly IMapper mapper;
        private readonly IBasketRepository repository;

        public BasketController(IPublishEndpoint publishEndpoint, DiscountgRPCService discountgRPC, 
            IMapper mapper, IBasketRepository repository) {
            this.publishEndpoint = publishEndpoint;
            this.discountgRPC = discountgRPC;
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpGet("{username}", Name= "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> getBasket(string userName) {
            var basket = await repository.GetBasket(userName);
            if (basket == null)
                return Ok(new ShoppingCart(userName));
            return Ok(basket);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> updateBasket([FromBody]ShoppingCart basket) {
            foreach (var item in basket.Items) {
                var coupon = await discountgRPC.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            await repository.UpdateBasket(basket);
            return Ok(basket);
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> deleteBasket(string userName) {
            await repository.DeleteBasket(userName);
            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout) {
            var basket = await repository.GetBasket(basketCheckout.UserName);
            if (basket == null)
                return BadRequest();

            var @event = mapper.Map<BasketCheckoutEvent>(basketCheckout);
            @event.TotalPrice = basket.TotalPrice;
            await publishEndpoint.Publish(@event);
            await repository.DeleteBasket(basketCheckout.UserName);
            return Accepted();
        }
    }
}
