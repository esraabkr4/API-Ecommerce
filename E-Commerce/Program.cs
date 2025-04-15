
using Domain.Contracts;
using E_Commerce.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using Services;
using Services.Abstraction;
using StackExchange.Redis;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
            #region Services
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            //builder.Services.AddScoped<IGenericRepository, GenericRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<IBasketService, BasketService>();

            //builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);

            builder.Services.AddDbContext<StoreContext>(options=> {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<StoreIdentityContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            #endregion
            builder.Services.AddSingleton<IConnectionMultiplexer>
                ( _=>ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            await IntializeDbAsync(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    
    public static async Task IntializeDbAsync(WebApplication app )
        {
            using var Scope=app.Services.CreateScope();
            var DbInitializer=Scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await DbInitializer.InitializeAsync();
        }
    }
}
