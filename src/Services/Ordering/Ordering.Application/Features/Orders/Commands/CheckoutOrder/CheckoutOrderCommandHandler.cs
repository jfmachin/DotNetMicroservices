using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder {
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int> {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, 
            IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger) {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.emailService = emailService;
            this.logger = logger;
        }
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken) {
            var order = mapper.Map<Order>(request);
            var newOrder = await orderRepository.AddAsync(order);
            logger.LogInformation($"Order {newOrder.Id} is successfully created");
            await SendMail(newOrder);
            return newOrder.Id;
        }

        private async Task SendMail(Order order) {
            var email = new Email() {
                To = "opennotice2015@gmail.com",
                Body = "Order was created",
                Subject = "Subject"
            };
            try {
                await emailService.SendEmail(email);
            }
            catch (Exception ex) {
                logger.LogError($"Error sending order with id {order.Id} due to {ex.Message}");
            }
        }
    }
}
