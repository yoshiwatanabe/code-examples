using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample
{
    public class Worker : IWorker
    {
        MyWorkerSettings settings;

        public Worker(IOptions<MyWorkerSettings> settings)
        {
            this.settings = settings.Value;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(1);
            Console.WriteLine("Do something useful here.");
        }
    }
}
