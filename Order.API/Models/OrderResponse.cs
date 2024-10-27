namespace Order.API.Models
{
    public class OrderResponse
    {
        public int status {  get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
