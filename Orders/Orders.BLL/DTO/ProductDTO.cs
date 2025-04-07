namespace Orders.BLL.DTO;

public record ProductDTO(
    Guid ProductID,
    string? ProductName,
    string? Category,
    double UnitPrice,
    int QuantityInStock);