using Cloud_Project.API.Middlewares;
using Cloud_Project.Application;
using Cloud_Project.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

//builder.Services
//    .AddApplicationServices(builder.Configuration)
//    .AddInfrastructureServices(builder.Configuration);


//// commands
//builder.Services.AddMediatR(typeof(CreatePackage).Assembly);
//builder.Services.AddMediatR(typeof(CreateDeliveryRequest).Assembly);
////builder.Services.AddMediatR(typeof(UpdateDeliveryStatus).Assembly);

//// queries
//builder.Services.AddMediatR(typeof(GetAllPackages).Assembly);
////builder.Services.AddMediatR(typeof(GetAllAssignedDeliveryRequests).Assembly);
////builder.Services.AddMediatR(typeof(GetAllFinishedDeliveryRequests).Assembly);

//builder.Services.AddDbContext<CloudDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseMiddleware<TokenBlacklistMiddleware>();


//Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers(); 


app.Run();


