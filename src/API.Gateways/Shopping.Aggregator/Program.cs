using Shopping.Aggregator.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<ICatalogService, CatalogService>(
    x => x.BaseAddress = new Uri(builder.Configuration["APIEndpoints:Catalog"]));
builder.Services.AddHttpClient<IBasketService, BasketService>(
    x => x.BaseAddress = new Uri(builder.Configuration["APIEndpoints:Basket"]));
builder.Services.AddHttpClient<IOrderService, OrderService>(
    x => x.BaseAddress = new Uri(builder.Configuration["APIEndpoints:Ordering"]));

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();