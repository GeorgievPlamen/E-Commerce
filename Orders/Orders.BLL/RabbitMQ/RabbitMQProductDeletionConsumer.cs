using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Orders.BLL.RabbitMQ;

public class RabbitMQProductDeletionConsumer : IDisposable, IRabbitMQProductDeletionConsumer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IConfiguration _configuration;
    private const string RoutingKey = "product.delete";
    private const string Queue = "orders.product.delete.queue";

    public RabbitMQProductDeletionConsumer(IConfiguration configuration)
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

    public void Consume()
    {

        var exchangeName = _configuration["RABBITMQ_Products_Exchange"];

        _channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);

        _channel.QueueDeclare(Queue, true, false, false, null);

        _channel.QueueBind(Queue, exchangeName, RoutingKey);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (sender, args) =>
        {
            System.Console.WriteLine("Event received");
            var byteArray = args.Body.ToArray();
            var messageJson = Encoding.UTF8.GetString(byteArray);
            var message = JsonSerializer.Deserialize<ProductNameUpdateMessage>(messageJson);
            System.Console.WriteLine("Message " + message);
        };

        _channel.BasicConsume(Queue, true, consumer);
    }

    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
    }
}
