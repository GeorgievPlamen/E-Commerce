using FluentValidation;
using Products.BLL.DTO;

namespace Products.BLL.Validators;

public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
{
    public ProductUpdateRequestValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty();

        RuleFor(x => x.Category)
            .IsInEnum();

        RuleFor(x => x.UnitPrice)
            .InclusiveBetween(0, double.MaxValue);

        RuleFor(x => x.QuantityInStock)
            .InclusiveBetween(0, int.MaxValue);
    }
}