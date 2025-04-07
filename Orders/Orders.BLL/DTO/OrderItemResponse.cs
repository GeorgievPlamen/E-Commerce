namespace Orders.BLL.DTO;

public record OrderItemResponse(Guid ProductID, decimal UnitPrice, int Quantity, decimal TotalPrice, string? ProductName, string? Category)
{
  public OrderItemResponse() : this(Guid.Empty, default, default, default, default, default)
  {
  }
}
