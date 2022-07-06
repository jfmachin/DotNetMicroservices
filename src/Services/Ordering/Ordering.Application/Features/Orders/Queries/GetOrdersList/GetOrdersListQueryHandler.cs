using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList {
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersDTO>> {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper) {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public async Task<List<OrdersDTO>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken) {
            var list = await orderRepository.GetOrdersByUserName(request.userName);
            var mapped = mapper.Map<List<OrdersDTO>>(list);
            return mapped;
        }
    }
}
