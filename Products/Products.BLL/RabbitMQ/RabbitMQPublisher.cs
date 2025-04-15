using Microsoft.Extensions.Configuration;

namespace Products.BLL.RabbitMQ;

public class RabbitMQPublisher(IConfiguration configuration) : IRabbitMQPublisher
{
    private readonly string hostname = configuration["RABBITMQ_Hostname"];
    private readonly string username = configuration["RABBITMQ_Username"];
    private readonly string password = configuration["RABBITMQ_Password"];
    private readonly string port = configuration["RABBITMQ_Port"];

    public void Publish<T>(string routingKey, T message)
    {
        throw new NotImplementedException();
    }
}
