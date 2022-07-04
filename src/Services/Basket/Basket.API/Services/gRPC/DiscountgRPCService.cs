using Discount.gRPC.Protos;
using static Discount.gRPC.Protos.DiscountProtoService;

namespace Basket.API.Services.gRPC {
    public class DiscountgRPCService {
        private readonly DiscountProtoServiceClient discountProtoService;

        public DiscountgRPCService(DiscountProtoServiceClient discountProtoService) {
            this.discountProtoService = discountProtoService;
        }

        public async Task<CouponModel> GetDiscount(string productName) {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await discountProtoService.GetDiscountAsync(discountRequest);
        }
    }
}
