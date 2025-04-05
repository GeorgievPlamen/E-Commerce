namespace Orders.BLL.DTO;

public record OrderItemUpdateRequest(Guid ProductID, decimal UnitPrice, int Quantity)
{
  public OrderItemUpdateRequest() : this(Guid.Empty, default, default)
  {
  }
}
