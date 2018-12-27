using System.Threading;
using System.Threading.Tasks;

namespace Sample
{
    public interface IWorker
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
