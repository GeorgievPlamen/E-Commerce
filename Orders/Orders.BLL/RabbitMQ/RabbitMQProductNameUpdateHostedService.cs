using Microsoft.Extensions.Hosting;

namespace Orders.BLL.RabbitMQ;

public class RabbitMQProductNameUpdateHostedService(
    IRabbitMQProductNameConsumer rabbitMQProductNameConsumer) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        rabbitMQProductNameConsumer.Consume();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        rabbitMQProductNameConsumer.Dispose();
        return Task.CompletedTask;
    }
}
