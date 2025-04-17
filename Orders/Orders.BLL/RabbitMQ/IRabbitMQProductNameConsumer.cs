namespace Orders.BLL.RabbitMQ;

public interface IRabbitMQProductNameConsumer
{
    void Consume();
    void Dispose();
}