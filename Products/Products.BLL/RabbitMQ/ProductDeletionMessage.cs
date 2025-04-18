namespace Products.BLL.RabbitMQ;

public record ProductDeletionMessage(
    Guid ProductID,
    string? ProductName);