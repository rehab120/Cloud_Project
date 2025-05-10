using Cloud_Project.Application.Command.AddRole;
using Cloud_Project.Application.Command.LoginMerchant;
using Cloud_Project.Application.Command.RegisterMerchant;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;



namespace Cloud_Project.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMediatR(cfg =>
            //{
            //    cfg.RegisterServicesFromAssembly(typeof(AddRoleCommand).Assembly);
            //    cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly);
            //    cfg.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly);
            //});

            services.AddMediatR(
                typeof(AddRoleCommand).Assembly,
                typeof(RegisterCommand).Assembly,
                typeof(LoginCommand).Assembly,

                // package
                typeof(CreatePackage).Assembly,
                typeof(GetAllPackages).Assembly,
                typeof(GetPackageById).Assembly,
                typeof(UpdatePackage).Assembly,
                typeof(DeletePackage).Assembly,
                typeof(GetAllUnattachedPackages).Assembly,

                // delivery
                typeof(CreateDeliveryRequest).Assembly,
                typeof(GetAllAssignedDeliveries).Assembly,
                typeof(GetAllDeliveries).Assembly,
                typeof(GetAllDeliveriesByMerchantId).Assembly,
                typeof(GetAllDeliveriesByDeliveryPersonId).Assembly,
                typeof(GetAllFinishedDeliveries).Assembly,
                typeof(GetDeliveryById).Assembly,
                typeof(UpdateDeliveryStatus).Assembly,
                typeof(DeleteDelivery).Assembly
            );

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],

                    ValidateAudience = false,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,


                };
            });

            return services;
        }
    }
}
