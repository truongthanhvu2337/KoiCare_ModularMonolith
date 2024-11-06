using EventBus.Events;
using MassTransit;
using Product.API.Services;

namespace Product.API.Consumer
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ILogger<OrderCreatedConsumer> _logger;
        private readonly ProductService _productService;

        public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            _logger.LogInformation("Product service has receive the message");

            var orderEvent = context.Message;

            var product = _productService.GetProductById((int)orderEvent.ProductId!);
            if (product != null && product.QuantityInStock >= orderEvent.Quantity)
            {
                product.QuantityInStock -= (int)orderEvent.Quantity;
                await Task.CompletedTask;
            }
        }
    }
}
