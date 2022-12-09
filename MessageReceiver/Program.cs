using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Text.Json;

const string connectionString = "Endpoint=sb://yassin-test.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=OihX5BhcaCeodOkPoRT4L3rJFSXo2aN0Usjv4FMqrNU=";
const string queueName = "test-queue";
IQueueClient queueClient;

queueClient = new QueueClient(connectionString, queueName, ReceiveMode.PeekLock);

var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
{
    MaxConcurrentCalls = 1,
    AutoComplete = false
};

queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

Console.ReadLine();

await queueClient.CloseAsync();

async Task ProcessMessagesAsync(Message message, CancellationToken token)
{
    var jsonString = Encoding.UTF8.GetString(message.Body);
    var recivedMessages = JsonSerializer.Deserialize<List<string>>(jsonString);

    foreach (var recivedMessage in recivedMessages)
    {
        Console.WriteLine($"Message Received: {recivedMessage}");
    }

    await queueClient.CompleteAsync(message.SystemProperties.LockToken);
}

Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
{
    Console.WriteLine($"Message handler exception: {arg.Exception}");
    return Task.CompletedTask;
}