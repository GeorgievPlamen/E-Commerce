namespace Orders.BLL.ServiceContracts;

using MongoDB.Driver;
using Orders.BLL.DTO;
using Orders.DAL.Entities;

public interface IOrdersService
{
  Task<List<OrderResponse?>> GetOrders();
  Task<List<OrderResponse?>> GetOrdersByCondition(FilterDefinition<Order> filter);
  Task<OrderResponse?> GetOrderByCondition(FilterDefinition<Order> filter);
  Task<OrderResponse?> AddOrder(OrderAddRequest orderAddRequest);
  Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest);
  Task<bool> DeleteOrder(Guid orderID);
}