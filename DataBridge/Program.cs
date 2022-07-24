using DataBridge.Broker;
using DataBridge.Broker.Settings;
using DataBridge.Mongo;
using DataBridge.Mongo.Interfaces;
using DataBridge.Mongo.Settings;
using DataBridge.RabbitMQ.Interface;
using DataBridge.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MessageDbSettings>(
    builder.Configuration.GetSection("MessageDB"));
builder.Services.Configure<BrokerSettings>(
    builder.Configuration.GetSection("Broker"));

builder.Services.AddSingleton<IStorageMessage, StorageMessageRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.EnableAnnotations());
builder.Services.AddHostedService<RabbitMqService>();
builder.Services.AddSingleton<IBroker, RabbitMQBroker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
