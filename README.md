# B2BrokerTask:

I used Azure Service Bus for sending messages to Message Bus with bufferization, it sends messages when the buffer size reaches a threshold. 
That way the application will be thread safe.

# Project Structure:
I divided the project into two solutions: 
- B2Broker task (Which will send the messages). 
- Message Receiver (Which will receive the sent messages).

# Tools:
I used C# Console Application using .net core 7.
