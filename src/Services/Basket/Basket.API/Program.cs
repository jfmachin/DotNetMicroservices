using Basket.API.Services.gRPC;
using Discount.gRPC.Protos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddStackExchangeRedisCache(opt => {
    opt.Configuration = builder.Configuration.GetConnectionString("redis");
});

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
    o => o.Address = new Uri(builder.Configuration.GetConnectionString("discountURL"))
);

builder.Services.AddScoped<DiscountgRPCService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();