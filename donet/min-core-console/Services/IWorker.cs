using System.Threading;
using System.Threading.Tasks;

namespace Sample.Services
{
    public interface IWorker
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
