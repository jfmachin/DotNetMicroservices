using AutoMapper;
using Discount.gRPC.Data;
using Discount.gRPC.Models.Entities;
using Discount.gRPC.Protos;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services {
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase {
        private readonly ILogger<DiscountService> logger;
        private readonly CouponContext couponContext;
        private readonly IMapper mapper;

        public DiscountService(ILogger<DiscountService> logger, CouponContext couponContext, IMapper mapper) {
            this.logger = logger;
            this.couponContext = couponContext;
            this.mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context) {
            var coupon = await couponContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} not found"));

            var couponModel = mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context) {
            var coupon = mapper.Map<Coupon>(request.Coupon);
            await couponContext.Coupons.AddAsync(coupon);
            await couponContext.SaveChangesAsync();
            
            var couponModel = mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context) {
            var coupon = mapper.Map<Coupon>(request.Coupon);
            couponContext.Coupons.Update(coupon);
            await couponContext.SaveChangesAsync();

            var couponModel = mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context) {
            var coupon = await couponContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            var exists = coupon != null;
            if (exists) {
                couponContext.Coupons.Remove(coupon);
                await couponContext.SaveChangesAsync();
            }
            return new DeleteDiscountResponse{ Success = exists };
        }
    }
}
