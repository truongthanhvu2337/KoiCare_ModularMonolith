using MassTransit;
using Order.API.Services;
using Product.API;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<OrderService>();
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitMqConfig = builder.Configuration.GetSection("MessageBroker");

        cfg.Host("localhost", "/", h =>
        {
            h.Username("sa");
            h.Password("sa");
        });
    });
});

builder.Services.AddGrpcClient<ProductProto.ProductProtoClient>(o =>
{
    o.Address = new Uri("http://localhost:5001"); 
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//---------------------------------------------------------------

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
