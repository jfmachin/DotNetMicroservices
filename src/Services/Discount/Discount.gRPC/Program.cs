using Discount.gRPC.Data;
using Discount.gRPC.Extensions;
using Discount.gRPC.Models.MapperProfiles;
using Discount.gRPC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MapperProfiles));
builder.Services.AddDbContext<CouponContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("postgres")
));
builder.Services.AddGrpc();

var app = builder.Build().MigrateDatabase<CouponContext>();

app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();