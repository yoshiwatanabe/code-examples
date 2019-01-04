using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Settings;
using Sample.Services;
using Sample.Settings;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Sample
{
    public class GenericHost
    {
        public GenericHost()
        {
        }

        public IConfiguration Configuration { get; private set; }

        public void Run()
        {
            RunAsync(CancellationToken.None).GetAwaiter().GetResult();
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var host = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddEnvironmentVariables();
                })
                .ConfigureAppConfiguration((hostContext, configBuilder) =>
                {
                    Configuration = configBuilder
                        .SetBasePath(hostContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", false, true)
                        .AddUserSecrets<MyWorkerSettings>()
                        .Build();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddOptions()
                        .Configure<MyWorkerSettings>(Configuration.GetSection(nameof(MyWorkerSettings)))
                        .Configure<CustomSettings>(Configuration.GetSection(nameof(CustomSettings)))
                        .Configure<ParentSettings>(options => Configuration.GetSection("ParentSettings").Bind(options))
                        .AddSingleton<IWorker, Worker>();
                })
                .UseConsoleLifetime()
                .Build();

            using (host)
            {
                await host.StartAsync();
                var worker = host.Services.GetService<IWorker>();
                await worker.RunAsync(cancellationToken);
                await host.WaitForShutdownAsync();
            }
        }
    }
}
