namespace Orders.BLL.DTO;

public record OrderAddRequest(Guid UserID, DateTime OrderDate, List<OrderItemAddRequest> OrderItems)
{
  public OrderAddRequest() : this(Guid.Empty, default, default!)
  {
  }
}