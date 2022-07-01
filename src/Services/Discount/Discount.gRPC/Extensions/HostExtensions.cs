using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Extensions {
    public static class HostExtensions {
        public static WebApplication MigrateDatabase<T>(this WebApplication host) where T: DbContext {
            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<T>>();
                try {
                    logger.LogInformation("Migrating postgres db.");
                    var db = services.GetRequiredService<T>();
                    db.Database.Migrate();
                }
                catch (Exception ex) {
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
            return host;
        }
    }
}
