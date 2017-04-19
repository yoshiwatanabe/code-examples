using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncSamples
{
    class Sample05
    {
        // By default, all threads in console applications and Windows Services only have 
        // the default SynchronizationContext.
        // https://msdn.microsoft.com/magazine/gg598924.aspx

        private static async Task DelayAsync()
        {
            await Task.Delay(1000);
            Debug.WriteLine("The thread is IsThreadPoolThread thread: {0}", Thread.CurrentThread.IsThreadPoolThread);
            // After Delay finishes delaying, the remaining instructions will run on a random thread-pool thred
            // instead of the caller's thread.
            // Why? Because the SynchronizationContext that is configured for a Console app is Default (ThreadPool) SynchronizationContext type,
            // and it would queues asynchronouse delegate to ThreadPool.
            // Note that this is in contrast to how WinForm and ASP.NET's SynchronizationContext work: they do NOT schedule/queues
            // the asynchronous delegate to ThreadPool but instead try to use the thread of the SynchronizationContext (and if the caller
            // is blocked by calls such as Task.Wait and Thread.Sleep, then it would dead-lock.
        }

        // This method causes a deadlock when called in a GUI or ASP.NET context.
        public static void Demo_MethodThatWouldDeadlockInWinFormButOKinConsole()
        {
            Debug.WriteLine("The thread is IsThreadPoolThread thread: {0}", Thread.CurrentThread.IsThreadPoolThread);

            // Start the delay.
            var delayTask = DelayAsync();
            // Wait for the delay to complete.
            delayTask.Wait();
        }
    }
}
