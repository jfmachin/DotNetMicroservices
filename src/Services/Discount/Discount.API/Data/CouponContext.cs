using Discount.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Data {
    public class CouponContext: DbContext {
        public CouponContext(DbContextOptions options) : base(options) { }
        public DbSet<Coupon> Coupons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Coupon>().HasData(
              new Coupon { Id = 1, ProductName = "iPhone X", Description = "Some description", Amount = 85 },
              new Coupon { Id = 2, ProductName = "Samsung S22", Description = "Some description", Amount = 65 }
            );
        }
    }
}
