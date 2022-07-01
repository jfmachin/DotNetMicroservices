using AutoMapper;
using Discount.gRPC.Models.Entities;
using Discount.gRPC.Protos;

namespace Discount.gRPC.Models.MapperProfiles {
    public class MapperProfiles : Profile {
        public MapperProfiles() {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
