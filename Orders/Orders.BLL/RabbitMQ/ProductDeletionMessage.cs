namespace Orders.BLL.RabbitMQ;

public record ProductDeletionMessage(
    Guid ProductID,
    string? ProductName);