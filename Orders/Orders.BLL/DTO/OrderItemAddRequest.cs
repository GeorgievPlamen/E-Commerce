namespace Orders.BLL.DTO;

public record OrderItemAddRequest(Guid ProductID, decimal UnitPrice, int Quantity)
{
  public OrderItemAddRequest() : this(Guid.Empty, default, default)
  {
  }
}
