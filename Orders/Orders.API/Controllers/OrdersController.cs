using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Orders.BLL.DTO;
using Orders.BLL.ServiceContracts;
using Orders.DAL.Entities;

namespace Orders.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrdersService ordersService) : ControllerBase
{

    [HttpGet]
    public async Task<IEnumerable<OrderResponse>> Get()
    {
        var result = await ordersService.GetOrders();

        return result;
    }

    [HttpGet("search/orderid/{orderID}")]
    public async Task<OrderResponse> GetOrderByOrderID(Guid orderID)
    {
        var filter = Builders<Order>.Filter.Eq(x => x.OrderID, orderID);
        var result = await ordersService.GetOrderByCondition(filter);

        return result;
    }

    [HttpGet("search/productid/{productID}")]
    public async Task<IEnumerable<OrderResponse>> GetOrdersByProductID(Guid productID)
    {
        var filter = Builders<Order>.Filter.ElemMatch(
            x => x.OrderItems,
            Builders<OrderItem>.Filter.Eq(x => x.ProductID, productID));

        var result = await ordersService.GetOrdersByCondition(filter);

        return result;
    }

    [HttpGet("search/orderDate/{orderDate}")]
    public async Task<IEnumerable<OrderResponse>> GetOrdersByOrderDate(DateTime orderDate)
    {
        var filter = Builders<Order>.Filter.Eq(
            x => x.OrderDate.ToString("yyyy-MM-dd"),
            orderDate.ToString("yyyy-MM-dd"));

        var result = await ordersService.GetOrdersByCondition(filter);

        return result;
    }



    //POST api/Orders
    [HttpPost]
    public async Task<IActionResult> Post(OrderAddRequest orderAddRequest)
    {
        if (orderAddRequest == null)
        {
            return BadRequest("Invalid order data");
        }

        OrderResponse? orderResponse = await ordersService.AddOrder(orderAddRequest);

        if (orderResponse == null)
        {
            return Problem("Error in adding product");
        }


        return Created($"api/Orders/search/orderid/{orderResponse?.OrderID}", orderResponse);
    }


    //PUT api/Orders/{orderID}
    [HttpPut("{orderID}")]
    public async Task<IActionResult> Put(Guid orderID, OrderUpdateRequest orderUpdateRequest)
    {
        if (orderUpdateRequest == null)
        {
            return BadRequest("Invalid order data");
        }

        if (orderID != orderUpdateRequest.OrderID)
        {
            return BadRequest("OrderID in the URL doesn't match with the OrderID in the Request body");
        }

        OrderResponse? orderResponse = await ordersService.UpdateOrder(orderUpdateRequest);

        if (orderResponse == null)
        {
            return Problem("Error in adding product");
        }


        return Ok(orderResponse);
    }


    //DELETE api/Orders/{orderID}
    [HttpDelete("{orderID}")]
    public async Task<IActionResult> Delete(Guid orderID)
    {
        if (orderID == Guid.Empty)
        {
            return BadRequest("Invalid order ID");
        }

        bool isDeleted = await ordersService.DeleteOrder(orderID);

        if (!isDeleted)
        {
            return Problem("Error in adding product");
        }

        return Ok(isDeleted);
    }
}