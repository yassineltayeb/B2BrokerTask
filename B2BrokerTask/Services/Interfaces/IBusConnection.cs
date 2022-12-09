namespace B2BrokerTask.Services.Interfaces;

public interface IBusConnection
{
    Task PublishAsync<T>(T serviceBusMessage);
}
