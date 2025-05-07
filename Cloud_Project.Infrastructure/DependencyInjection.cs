using Cloud_Project.Application.Common.Interfaces;
using Cloud_Project.Domain.Interface;
using Cloud_Project.Infrastructure.Persistence.Repositories;
using Cloud_Project.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cloud_Project.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CloudDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Connection")));

            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<IIdGenerator, IdGenerator>();
            services.AddScoped<IDeliveryPersonRepositry, DeliveryPersonRepository>();
            services.AddScoped<IMerchantRepositry, MerchantRepository>();
            services.AddIdentity<IdentityUser, IdentityRole>(
                options =>
                {
                    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;

                }).AddEntityFrameworkStores<CloudDbContext>().AddDefaultTokenProviders();
            return services;
        }
    }
}
