using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Products.BLL.RabbitMQ;

public class RabbitMQPublisher : IRabbitMQPublisher
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQPublisher(IConfiguration configuration)
    {
        ConnectionFactory connectionFactory = new()
        {
            HostName = configuration["RABBITMQ_Hostname"],
            UserName = configuration["RABBITMQ_Username"],
            Password = configuration["RABBITMQ_Password"],
            Port = Convert.ToInt32(configuration["RABBITMQ_Port"])
        };

        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public async void Publish<T>(string routingKey, T message)
    {
        var messageJson = JsonSerializer.Serialize(message);
        var messageBytes = Encoding.UTF8.GetBytes(messageJson);

        var exchangeName = "products.exchange";

        _channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);

        _channel.BasicPublish(exchangeName, routingKey, null, messageBytes);
    }
}
