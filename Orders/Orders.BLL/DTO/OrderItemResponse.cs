namespace Orders.BLL.DTO;

public record OrderItemResponse(Guid ProductID, decimal UnitPrice, int Quantity, decimal TotalPrice)
{
  public OrderItemResponse() : this(Guid.Empty, default, default, default)
  {
  }
}
