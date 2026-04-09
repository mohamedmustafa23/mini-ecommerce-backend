using Microsoft.EntityFrameworkCore;
using MiniEcommerce.API.Data;
using MiniEcommerce.API.Repositories;
using MiniEcommerce.API.Services;
using MiniEcommerce.Core.Contracts;
using MiniEcommerce.Core.ServicesAbstraction;
using Scalar.AspNetCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IOrderService, OrderService>();

        builder.Services.AddCors(options => {
            options.AddPolicy("AllowBlazor", policy =>
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowBlazor");

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}