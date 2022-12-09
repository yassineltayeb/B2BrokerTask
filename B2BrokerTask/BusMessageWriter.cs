using B2BrokerTask.Services.Interfaces;
using B2BrokerTask.Services.Implementations;
using System.Text;

public class BusMessageWriter
{
    private readonly IBusConnection _connection;
    private readonly MemoryStream _buffer = new();
    private List<Object> queue;

    public BusMessageWriter()
    {
        _connection = new AzureServiceBus();
        queue = new List<Object>();
    }

    // how to make this method thread safe?
    public async Task<long> SendMessageAsync(byte[] nextMessage)
    {
        //Add Messages To Queue
        queue.Add(Encoding.UTF8.GetString(nextMessage));

        _buffer.Write(nextMessage, 0, nextMessage.Length);

        var bufferLength = _buffer.Length;

        if (_buffer.Length > 1000)
        {
            await _connection.PublishAsync(queue);
            _buffer.SetLength(0);
        }

        return bufferLength;
    }
}

