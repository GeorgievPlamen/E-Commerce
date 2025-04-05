using MongoDB.Driver;
using Orders.DAL.Contracts;
using Orders.DAL.Entities;

namespace Orders.DAL.Repositories;

public class OrdersRepository(IMongoDatabase db) : IOrdersRepository
{
    private readonly IMongoCollection<Order> _orders = db.GetCollection<Order>("orders");
    public async Task<Order?> AddOrder(Order order)
    {
        order.OrderID = Guid.NewGuid();

        await _orders.InsertOneAsync(order);

        return order;
    }

    public async Task<bool> DeleteOrder(Guid orderID)
    {
        var result = await _orders.DeleteOneAsync(x => x.OrderID == orderID);

        return result.DeletedCount > 0;

        // var filter = Builders<Order>.Filter.Eq(x => x.OrderID, orderID);
        // var existingOrder = await _orders.DeleteOneAsync(filter);
    }

    public async Task<Order?> GetOrderByCondition(FilterDefinition<Order> filter)
    {
        var result = await _orders.FindAsync(filter);

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        var result = await _orders.FindAsync(Builders<Order>.Filter.Empty);

        return result.ToList();
    }

    public async Task<IEnumerable<Order?>> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        var result = await _orders.FindAsync(filter);

        return result.ToList();
    }

    public async Task<Order?> UpdateOrder(Order order)
    {
        var result = await _orders.FindOneAndReplaceAsync(x => x.OrderID == order.OrderID, order);

        return result;
    }
}
