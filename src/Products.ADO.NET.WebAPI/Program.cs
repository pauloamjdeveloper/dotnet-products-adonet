using Products.ADO.NET.WebAPI.Business.Implementations;
using Products.ADO.NET.WebAPI.Business;
using Products.ADO.NET.WebAPI.Data.Implementations;
using Products.ADO.NET.WebAPI.Data;
using Products.ADO.NET.WebAPI.Repositories.Implementations;
using Products.ADO.NET.WebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConnectionDataBase, SQLServerDataBase>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductBusiness, ProductBusiness>();

builder.Services.AddCors();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
