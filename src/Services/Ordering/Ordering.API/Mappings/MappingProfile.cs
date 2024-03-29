﻿using AutoMapper;
using EventBus.Events;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.API.Mappings {
    public class MappingProfile: Profile {
        public MappingProfile() {
            CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
