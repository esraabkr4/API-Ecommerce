
using System.Collections.Immutable;
using System.Text;
using Domain.Contracts;
using Domain.Entities;
using E_Commerce.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using Services;
using Services.Abstraction;
using Shared.Security;
using StackExchange.Redis;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var _JwtOptions= builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            options.TokenValidationParameters=new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer=true,
                ValidateAudience=true,
                ValidateLifetime=true,
                ValidateIssuerSigningKey=true,
                ValidIssuer=_JwtOptions.Issure,
                ValidAudience=_JwtOptions.Audience,
                IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtOptions.SecretKey))
            });
            builder.Services.AddAuthorization();
            //builder.Services.AddIdentity<User, IdentityRole>()
            //                .AddEntityFrameworkStores<StoreIdentityContext>()
            //                .AddDefaultTokenProviders();

            builder.Services.AddIdentity<User, IdentityRole>(option =>
            {
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<StoreIdentityContext>()
            .AddDefaultTokenProviders();


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
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    
    public static async Task IntializeDbAsync(WebApplication app )
        {
            using var Scope=app.Services.CreateScope();
            var DbInitializer=Scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await DbInitializer.InitializeAsync();
            await DbInitializer.InitializeIdentityAsync();
        }
    }
}
