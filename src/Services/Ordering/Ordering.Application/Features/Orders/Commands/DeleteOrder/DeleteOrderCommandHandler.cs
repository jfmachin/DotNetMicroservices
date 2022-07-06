using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder {
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand> {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly ILogger<DeleteOrderCommandHandler> logger;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper,
            ILogger<DeleteOrderCommandHandler> logger) {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken) {
            var order = await orderRepository.GetByIdAsync(request.Id);
            if (order == null) 
                throw new NotFoundException(nameof(Order), request.Id);
            await orderRepository.DeleteAsync(order);
            return Unit.Value;
        }
    }
}
