using Catalog.API.Data;
using Catalog.API.Models.Mapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddControllers();
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