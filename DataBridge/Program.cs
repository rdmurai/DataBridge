using DataBridge.Broker;
using DataBridge.Broker.Settings;
using DataBridge.Infrastructure.Repository;
using DataBridge.Infrastructure.Repository.Interfaces;
using DataBridge.Mongo;
using DataBridge.Mongo.Interfaces;
using DataBridge.Mongo.Settings;
using DataBridge.RabbitMQ.Interface;
using DataBridge.Services;
using DataBridge.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var authSection = builder.Configuration.GetSection("Authentication");
builder.Services.Configure<AuthenticationSettings>(authSection);
var authSettings = authSection.Get<AuthenticationSettings>();
var key = Encoding.UTF8.GetBytes(authSettings.Key);


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
})
    ;

builder.Services.Configure<MessageDbSettings>(
    builder.Configuration.GetSection("MessageDB"));
builder.Services.Configure<BrokerSettings>(
    builder.Configuration.GetSection("Broker"));

builder.Services.AddSingleton<IStorageMessage, StorageMessageRepository>();
builder.Services.AddSingleton<IUser, UserRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.EnableAnnotations());
builder.Services.AddHostedService<RabbitMqService>();
builder.Services.AddTransient<TokenServices>();
builder.Services.AddSingleton<IBroker, RabbitMQBroker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
