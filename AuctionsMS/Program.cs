using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using AuctionMS.Application.Handlers.Commands;
using AuctionMS.Core.DataBase;
using AuctionMS.Core.Repositories;
using AuctionMS.Core.Service;
using AuctionMS.Infrastructure.DataBase;
using AuctionMS.Infrastructure.Repositories;
using AuctionMS.Infrastructure.Setings;
using System.Configuration;
using MassTransit;
using AuctionMS.Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;
using ProductMS.Infrastructure.Messaging.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var _appSettings = new AppSettings();
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
_appSettings = appSettingsSection.Get<AppSettings>();
builder.Services.Configure<AppSettings>(appSettingsSection);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddHttpClient();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAuctionCommandHandler).Assembly));

builder.Services.AddScoped<IEventPublisher, EventPublisher>();
builder.Services.AddTransient<IAuctionsDbContext, AuctionsDbContext>();
builder.Services.AddTransient<IAuctionRepository, AuctionRepository>();
builder.Services.AddTransient<IAuctionsDbContext, AuctionsDbContext>();

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton(provider =>
{
    var context = provider.GetRequiredService<MongoDbContext>();
    return context.Auctions;
});

var dbConnectionString = builder.Configuration.GetValue<string>("DefaultConnection");
builder.Services.AddDbContext<AuctionsDbContext>(options =>
options.UseSqlServer(dbConnectionString));
builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<AuctionCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("auction-created-queue", e =>
        {
            e.ConfigureConsumer<AuctionCreatedConsumer>(context);
        });
    });
});


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();  
app.MapControllers();
app.Run();

