namespace Orders.BLL.RabbitMQ;

public interface IRabbitMQProductDeletionConsumer
{
    void Consume();
    void Dispose();
}