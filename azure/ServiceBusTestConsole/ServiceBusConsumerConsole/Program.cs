using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.ServiceBus.Messaging;


namespace ServiceBusConsumerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Endpoint=sb://yoshi-investigation.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=f4waL/CCFfVDNBoT8QFfZTzDKCoYD7UHHoK73vCA7v0="; 
            var queueName = "q1";

            var client = QueueClient.CreateFromConnectionString(connectionString, queueName);

            client.OnMessage(message =>
            {
                Console.WriteLine(String.Format("Message body: {0}", message.GetBody<String>()));
                Console.WriteLine(String.Format("Message id: {0}", message.MessageId));
            });

            Console.ReadLine();


        }
    }
}
