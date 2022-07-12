using AutoMapper;
using Basket.API.Models.Entities;
using EventBus.Events;

namespace Basket.API.Mappings {
    public class MappingProfile: Profile {
        public MappingProfile() {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}