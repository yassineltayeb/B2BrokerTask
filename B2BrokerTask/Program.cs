using System.Text;
using System.Text.Json;

Console.WriteLine("Start Sending Messages");

var message ="Test Message";

Console.WriteLine($"Message :{message}");

var serializedMessage = JsonSerializer.Serialize(message);

byte[] bytes = Encoding.ASCII.GetBytes(serializedMessage);

var busMessageWriter = new BusMessageWriter();

long bufferLength = 0;

do
{
    bufferLength = await busMessageWriter.SendMessageAsync(bytes);
    Console.WriteLine($"Buffer Length: {bufferLength}");
} while (bufferLength < 1000);

Console.WriteLine("Messages Sent");
