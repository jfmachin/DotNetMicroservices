using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence {
    public class OrderContextSeed {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger) {
            if (!orderContext.Orders.Any()) {
                orderContext.Orders.AddRangeAsync(new List<Order> {
                    new Order() {UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", 
                        EmailAddress = "ezozkme@gmail.com", AddressLine = "Bahcelievler", 
                        Country = "Turkey", TotalPrice = 350 }
                });
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seeding database Ok!");
            }
        }
    }
}
