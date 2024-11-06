using MassTransit;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Product.API.Consumer;
using Product.API.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ProductService>();

builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(5001, listenOptions => // HTTP for Web API
    {
        listenOptions.Protocols = HttpProtocols.Http1; // HTTP/1.1
    });

    options.ListenAnyIP(5295, listenOptions => // HTTPS for gRPC
    {
        listenOptions.UseHttps(); // Ensure HTTPS is used
        listenOptions.Protocols = HttpProtocols.Http2; // HTTP/2
    });
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>(); 

    x.UsingRabbitMq((context, cfg) =>
    {

        cfg.Host("localhost", "/", h =>
        {
            h.Username("sa");
            h.Password("sa");
        });

        cfg.ReceiveEndpoint("order-created-queue", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(context);
        });
    });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ProductServiceProto>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
