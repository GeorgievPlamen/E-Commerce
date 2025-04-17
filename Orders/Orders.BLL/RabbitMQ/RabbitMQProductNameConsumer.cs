using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Orders.BLL.RabbitMQ;

public class RabbitMQProductNameConsumer : IRabbitMQProductNameConsumer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IConfiguration _configuration;
    private const string RoutingKey = "product.update.name";
    private const string Queue = "orders.product.update.queue";

    public RabbitMQProductNameConsumer(IConfiguration configuration)
    {
        ConnectionFactory connectionFactory = new()
        {
            HostName = configuration["RABBITMQ_Hostname"],
            UserName = configuration["RABBITMQ_Username"],
            Password = configuration["RABBITMQ_Password"],
            Port = Convert.ToInt32(configuration["RABBITMQ_Port"])
        };

        _configuration = configuration;
        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public async void Consume<T>(T message)
    {

        var exchangeName = _configuration["RABBITMQ_Products_Exchange"];

        _channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);

        _channel.QueueDeclare(Queue, true, false, false, null);

        _channel.QueueBind(Queue, exchangeName, RoutingKey);
    }
}
