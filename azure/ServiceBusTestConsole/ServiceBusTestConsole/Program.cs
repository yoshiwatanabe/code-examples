using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.ServiceBus.Messaging;


namespace ServiceBusTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Endpoint=sb://yoshi-investigation.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=f4waL/CCFfVDNBoT8QFfZTzDKCoYD7UHHoK73vCA7v0=";
            var queueName = "q1";

            var client = QueueClient.CreateFromConnectionString(connectionString, queueName);
            var message = new BrokeredMessage("This is a test message!");
            client.Send(message);

        }
    }
}
