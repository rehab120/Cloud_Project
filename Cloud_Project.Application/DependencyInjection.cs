using Cloud_Project.Application.Commond.AddRole;
using Cloud_Project.Application.Commond.LoginMerchant;
using Cloud_Project.Application.Commond.RegisterMerchant;
using Cloud_Project.Application.Usecase.Commands;
using Cloud_Project.Application.Usecase.Queries;
using Cloud_Project.Application.Usecase.Queries.GetDeliveryById;
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

                // delivery
                typeof(CreateDeliveryRequest).Assembly,
                typeof(GetAllAssignedDeliveries).Assembly,
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
