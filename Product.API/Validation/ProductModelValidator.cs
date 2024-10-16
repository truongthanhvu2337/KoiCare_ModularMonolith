using FluentValidation;

namespace Product.API.Validation
{
    public class ProductModelValidator : AbstractValidator<ProductModel>
    {
        public ProductModelValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(1, 100).WithMessage("Product name must be between 1 and 100 characters.");

            RuleFor(product => product.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(product => product.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(product => product.QuantityInStock)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity in stock cannot be negative.");

            RuleFor(product => product.Supplier)
                .NotEmpty().WithMessage("Supplier is required.")
                .Length(1, 100).WithMessage("Supplier name must be between 1 and 100 characters.");

            RuleFor(product => product.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0.");

            RuleFor(product => product.Dimensions)
                .NotEmpty().WithMessage("Dimensions are required.")
                .Length(1, 50).WithMessage("Dimensions must be between 1 and 50 characters.");
        }
    }

}
