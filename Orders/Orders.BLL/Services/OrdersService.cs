using AutoMapper;
using MongoDB.Driver;
using Orders.BLL.DTO;
using Orders.BLL.HttpClients;
using Orders.BLL.ServiceContracts;
using Orders.DAL.Contracts;
using Orders.DAL.Entities;

namespace Orders.BLL.Services;

public class OrdersService(
    UsersMicroserviceClient usersMicroserviceClient,
    IOrdersRepository ordersRepository,
    IMapper mapper) : IOrdersService
{
    public async Task<OrderResponse?> AddOrder(OrderAddRequest orderAddRequest)
    {
        var user = await usersMicroserviceClient.GetUserByUserID(orderAddRequest.UserID);

        ArgumentNullException.ThrowIfNull(user);

        var order = mapper.Map<Order>(orderAddRequest);

        foreach (var item in order.OrderItems)
        {
            item.TotalPrice = item.Quantity * item.UnitPrice;
        }

        order.TotalBill = order.OrderItems.Sum(x => x.TotalPrice);

        var result = mapper.Map<OrderResponse?>(await ordersRepository.AddOrder(order));

        return result;
    }

    public async Task<bool> DeleteOrder(Guid orderID)
    {
        return await ordersRepository.DeleteOrder(orderID);
    }

    public async Task<OrderResponse?> GetOrderByCondition(FilterDefinition<Order> filter)
    {
        var result = mapper.Map<OrderResponse?>(await ordersRepository.GetOrderByCondition(filter));

        return result;
    }

    public async Task<List<OrderResponse?>> GetOrders()
    {
        var result = mapper.Map<List<OrderResponse?>>(await ordersRepository.GetOrders());

        return result;
    }

    public async Task<List<OrderResponse?>> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        var result = mapper.Map<List<OrderResponse?>>(await ordersRepository.GetOrdersByCondition(filter));

        return result;
    }

    public async Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest)
    {
        var order = mapper.Map<Order>(orderUpdateRequest);

        foreach (var item in order.OrderItems)
        {
            item.TotalPrice = item.Quantity * item.UnitPrice;
        }

        order.TotalBill = order.OrderItems.Sum(x => x.TotalPrice);

        var user = await usersMicroserviceClient.GetUserByUserID(orderUpdateRequest.UserID);

        ArgumentNullException.ThrowIfNull(user);

        var result = mapper.Map<OrderResponse?>(await ordersRepository.UpdateOrder(order));

        return result;
    }
}
