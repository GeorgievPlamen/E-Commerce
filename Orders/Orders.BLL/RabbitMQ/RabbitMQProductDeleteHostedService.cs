using Microsoft.Extensions.Hosting;

namespace Orders.BLL.RabbitMQ;

public class RabbitMQProductDeleteHostedService(
    IRabbitMQProductDeletionConsumer rabbitMQDeletionConsumer) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        rabbitMQDeletionConsumer.Consume();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        rabbitMQDeletionConsumer.Dispose();
        return Task.CompletedTask;
    }
}
