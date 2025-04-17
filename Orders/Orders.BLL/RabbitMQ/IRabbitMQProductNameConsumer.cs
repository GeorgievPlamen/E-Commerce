namespace Orders.BLL.RabbitMQ;

public interface IRabbitMQProductNameConsumer
{
    void Consume<T>(T message);
}