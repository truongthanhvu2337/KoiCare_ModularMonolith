using Grpc.Core;
using Product.API;
using Product.API.Validation;

namespace Product.API.Services;

public class ProductService : ProductProto.ProductProtoBase
{

    private static readonly List<ProductModel> _products = new List<ProductModel>
    {
        new ProductModel
        {
            ProductId = 2,
            Name = "Bình lọc nước cá Koi",
            Description = "Bình lọc nước hiệu quả, giúp giữ cho hồ cá luôn sạch sẽ.",
            Price = 150.00,
            Category = "Thiết bị",
            QuantityInStock = 20,
            Supplier = "AquaTech",
            Weight = 10.0,
            Dimensions = "50 x 30 x 30 cm",
            IsAvailable = true,
            CreatedAt = DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd"),
            UpdatedAt = DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd")
        },
        new ProductModel
        {
            ProductId = 3,
            Name = "Sát trùng hồ cá Koi",
            Description = "Chất sát trùng an toàn cho cá, giúp phòng ngừa bệnh tật.",
            Price = 18.00,
            Category = "Chăm sóc",
            QuantityInStock = 50,
            Supplier = "Koi Health",
            Weight = 0.5,
            Dimensions = "10 x 10 x 5 cm",
            IsAvailable = true,
            CreatedAt = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"),
            UpdatedAt = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd")
        }
    };

    public override Task<ProductModel> GetProduct(GetProductRequest request, ServerCallContext context)
    {
        var product = _products.Find(p => p.ProductId == request.ProductId);
        if (product == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));
        }
        return Task.FromResult(product);
    }

    public override Task<ProductListResponse> ListProducts(EmptyRequest request, ServerCallContext context)
    {
        var response = new ProductListResponse();
        response.Products.AddRange(_products);
        return Task.FromResult(response);
    }

    // Create New Product
    public override Task<ProductResponse> CreateProduct(ProductModel request, ServerCallContext context)
    {
        var validator = new ProductModelValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new RpcException(new Status(StatusCode.InvalidArgument, $"Validation failed: {errors}"));
        }
        request.ProductId = _products.Max(a => a.ProductId) + 1;
        request.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd");
        request.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd");
        _products.Add(request);

        return Task.FromResult(new ProductResponse { Status = "Product created successfully" });
    }

    // Update Existing Product
    public override Task<ProductResponse> UpdateProduct(ProductModel request, ServerCallContext context)
    {
        var product = _products.Find(p => p.ProductId == request.ProductId);
        if (product == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Category = request.Category;
        product.QuantityInStock = request.QuantityInStock;
        product.Supplier = request.Supplier;
        product.Weight = request.Weight;
        product.Dimensions = request.Dimensions;
        product.IsAvailable = request.IsAvailable;
        product.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd");

        return Task.FromResult(new ProductResponse { Status = "Product updated successfully" });
    }

    // Delete Product by ID
    public override Task<ProductResponse> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
    {
        var product = _products.Find(p => p.ProductId == request.ProductId);
        if (product == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));
        }

        _products.Remove(product);
        return Task.FromResult(new ProductResponse { Status = "Product deleted successfully" });
    }
}
