using Order.API.Models;
using Product.API;
using Grpc.Core;

namespace Order.API.Services
{
    public class OrderService
    {
        private readonly ProductProto.ProductProtoClient _client;
        private static readonly List<Order.API.Models.Order> _orders = new List<Order.API.Models.Order>
        {
            new Order.API.Models.Order
            {
                OrderId = 1,
                ProductId = 2,
                ProductName = "Sample Product",
                Description = "This is a sample product description",
                Price = 100m,
                Quantity = 2,
                TotalPrice = 200m,
                CreatedAt = DateTime.Now.AddDays(-1),
                UpdatedAt = DateTime.Now,
                IsCompleted = false,
                CustomerName = "John Doe",
                ShippingAddress = "123 Main St, City, Country",
                ShippingDate = DateTime.Now.AddDays(3),
                OrderStatus = "Processing"
            }
        };

        public OrderService(ProductProto.ProductProtoClient client)
        {
            _client = client;
        }


        public async Task<OrderResponse> CreateOrder(Order.API.Models.Order order)
        {
            try
            {
                var productDetails = await _client.GetProductAsync(
                    new GetProductRequest
                    {
                        ProductId = order.ProductId.Value,
                    });

                // You can assume that if the gRPC call succeeds, the product details will not be null
                order.ProductName = productDetails.Name;
                order.Description = productDetails.Description;
                order.Price = (decimal)productDetails.Price;
                order.TotalPrice = order.Price * order.Quantity;

                _orders.Add(order);

                return new OrderResponse
                {
                    status = 200,
                    message = "Order created successfully",
                    data = order
                };
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
            {
                return new OrderResponse
                {
                    status = 404,
                    message = "Product not found",
                };
            }
        }

        public async Task<OrderResponse> GetAll()
        {
            return new OrderResponse
            {
                status = 200,
                message = "Order retrieved successfully",
                data = _orders
            };
        }
    }
}
