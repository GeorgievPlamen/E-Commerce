namespace Products.BLL.DTO;

public record ProductUpdateRequest(
    Guid ProductID,
    string ProductName,
    CategoryOptions Category,
    double? UnitPrice,
    int? QuantityInStock)
{
    public ProductUpdateRequest() : this(Guid.Empty, default!, default, default, default) { }
};