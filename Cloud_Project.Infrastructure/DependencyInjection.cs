using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloud_Project.Domain.Interface;
using Cloud_Project.Infrastructure.Identity;
using Cloud_Project.Infrastructure.Persistence.Repositories;
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

            services.AddScoped<IDeliveryRepositry, DeliveryRepositry>();
            services.AddScoped<IDeliveryPersonRepositry, DeliveryPersonRepositry>();
            services.AddScoped<IMerchantRepositry, MerchantRepositry>();
            services.AddIdentity<IdentityUser, IdentityRole>(
                options =>
                {
                    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;

                }).AddEntityFrameworkStores<CloudDbContext>().AddDefaultTokenProviders();
            return services;
        }
    }
}
