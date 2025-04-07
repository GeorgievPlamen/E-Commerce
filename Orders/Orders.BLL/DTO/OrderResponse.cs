namespace Orders.BLL.DTO;

public record OrderResponse(Guid OrderID, Guid UserID, decimal TotalBill, DateTime OrderDate, List<OrderItemResponse> OrderItems, string? UserPersonName, string? Email)
{
  public OrderResponse() : this(Guid.Empty, Guid.Empty, default, default, default!, default, default)
  {
  }
}

