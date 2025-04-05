namespace Orders.BLL.DTO;

public record OrderUpdateRequest(Guid OrderID, Guid UserID, DateTime OrderDate, List<OrderItemAddRequest> OrderItems)
{
  public OrderUpdateRequest() : this(Guid.Empty, Guid.Empty, default, default!)
  {
  }
}


