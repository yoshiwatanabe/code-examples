using Microsoft.Extensions.Options;
using Project.Settings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample
{
    public class Worker : IWorker
    {
        MyWorkerSettings myWorkerSettings;
        CustomSettings customSettings;
        ParentSettings parentSettings;

        public Worker(
            IOptions<MyWorkerSettings> myWorkerSettings,
            IOptions<CustomSettings> customSettings,
            IOptions<ParentSettings> parentSettings)
        {
            this.myWorkerSettings = myWorkerSettings.Value;
            this.customSettings = customSettings.Value;
            this.parentSettings = parentSettings.Value;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(1);
            Console.WriteLine("Do something useful here.");
        }
    }
}
