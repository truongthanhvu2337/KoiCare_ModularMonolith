namespace Order.API.Models
{
    public class Order
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }  
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } 
        public int? Quantity { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool? IsCompleted { get; set; }
        public string? CustomerName { get; set; }
        public string? ShippingAddress { get; set; }
        public DateTime ShippingDate { get; set; }
        public string? OrderStatus { get; set; }
    }
}
