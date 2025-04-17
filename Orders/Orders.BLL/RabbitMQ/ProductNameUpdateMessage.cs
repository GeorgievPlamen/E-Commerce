namespace Orders.BLL.RabbitMQ;

public record ProductNameUpdateMessage(
    Guid ProductID,
    string? ProductName);