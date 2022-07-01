using Discount.API.Data;
using Discount.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Catalog.API.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase {
        private readonly CouponContext couponContext;
        private readonly ILogger logger;
        
        public DiscountController(CouponContext couponContext, ILogger<DiscountController> logger) {
            this.couponContext = couponContext;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Coupon>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetDiscounts() {
            var coupons = await couponContext.Coupons.AsQueryable().ToListAsync();
            return Ok(coupons);
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName) {
            var coupon = await couponContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == productName);
            if (coupon == null)
                return new Coupon {Amount=0, ProductName="No discount", Description="No discount desc." };
            return Ok(coupon);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon) {
            await couponContext.Coupons.AddAsync(coupon);
            await couponContext.SaveChangesAsync();
            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateDiscount([FromBody] Coupon coupon) {
            couponContext.Coupons.Update(coupon);
            await couponContext.SaveChangesAsync();
            return Ok(coupon);
        }

        [HttpDelete("{productName}")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteDiscount(string productName) {
            var coupon = await couponContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == productName);
            if (coupon == null)
                return NotFound(productName);
            couponContext.Coupons.Remove(coupon);
            await couponContext.SaveChangesAsync();
            return Ok(coupon);
        }
    }
}