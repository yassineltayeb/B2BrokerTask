using System.Text.Json;
using System.Text;
using Microsoft.Azure.ServiceBus;
using B2BrokerTask.Services.Interfaces;

namespace B2BrokerTask.Services.Implementations;

public class AzureServiceBus : IBusConnection
{

    const string connectionString = "Endpoint=sb://yassin-test.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=OihX5BhcaCeodOkPoRT4L3rJFSXo2aN0Usjv4FMqrNU=";
    const string queueName = "test-queue";

    public async Task PublishAsync<T>(T serviceBusMessage)
    {
        var queueClient = new QueueClient(connectionString, queueName);
        var messageBody = JsonSerializer.Serialize(serviceBusMessage);
        var message = new Message(Encoding.UTF8.GetBytes(messageBody));
        await queueClient.SendAsync(message);
    }
}
