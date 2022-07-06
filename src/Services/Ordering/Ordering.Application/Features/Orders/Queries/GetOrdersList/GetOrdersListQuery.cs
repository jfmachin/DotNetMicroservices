using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList {
    public class GetOrdersListQuery : IRequest<List<OrdersDTO>> {
        public readonly string userName;

        public GetOrdersListQuery(string userName) {
            this.userName = userName;
        }
    }
}
