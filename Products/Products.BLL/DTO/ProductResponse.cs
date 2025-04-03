namespace Products.BLL.DTO;

public record ProductResponse(
    Guid ProductID,
    string ProductName,
    CategoryOptions Category,
    double? UnitPrice,
    int? QuantityInStock)
{
    public ProductResponse() : this(Guid.Empty, default!, default, default, default) { }
};