namespace Product.API.Services;

public class ProductService 
{

    private static readonly List<Models.Product> _products = new List<Models.Product>
    {
        new Models.Product
        {
            ProductID = 2,
            Name = "Bình lọc nước cá Koi",
            Description = "Bình lọc nước hiệu quả, giúp giữ cho hồ cá luôn sạch sẽ.",
            Price = 150.00M,
            Category = "Thiết bị",
            QuantityInStock = 20,
            Supplier = "AquaTech",
            Weight = 10.0M,
            Dimensions = "50 x 30 x 30 cm",
            IsAvailable = true,
            CreatedAt = DateTime.Now.AddDays(-4),
            UpdatedAt = DateTime.Now.AddDays(-4)
        },
        new Models.Product
        {
            ProductID = 3,
            Name = "Sát trùng hồ cá Koi",
            Description = "Chất sát trùng an toàn cho cá, giúp phòng ngừa bệnh tật.",
            Price = 18.00M,
            Category = "Chăm sóc",
            QuantityInStock = 50,
            Supplier = "Koi Health",
            Weight = 0.5M,
            Dimensions = "10 x 10 x 5 cm",
            IsAvailable = true,
            CreatedAt = DateTime.Now.AddDays(-3),
            UpdatedAt = DateTime.Now.AddDays(-3)
        }
    };


    public Models.Product GetProductById(int productId)
    {
        return _products.Find(p => p.ProductID == productId);
    }

    public IEnumerable<Models.Product> GetAllProduct()
    {
        return _products.ToList();
    }

}
