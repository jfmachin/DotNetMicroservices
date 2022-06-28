using Basket.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Net;

namespace Basket.API.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase {
        private readonly IDistributedCache redisCache;

        public BasketController(IDistributedCache redisCache) {
            this.redisCache = redisCache;
        }

        [HttpGet("{username}", Name= "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> getBasket(string userName) {
            var basket = await redisCache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(basket))
                return Ok(new ShoppingCart(userName));
            var basketObject = JsonConvert.DeserializeObject<ShoppingCart>(basket);
            return Ok(basketObject);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> updateBasket([FromBody]ShoppingCart basket) {
            await redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return Ok(basket);
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> deleteBasket(string userName) {
            await redisCache.RemoveAsync(userName);
            return Ok();
        }
    }
}
