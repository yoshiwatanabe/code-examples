using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncSamples
{
    class Sample02
    {
        public static void Demo()
        {
            Console.WriteLine("1 on thread {0}", Thread.CurrentThread.ManagedThreadId);

            Task<int> task = GetTotalAsync();

            Console.WriteLine("5 or 4 on thread {0}", Thread.CurrentThread.ManagedThreadId);

            int total = task.Result; // Blocks, give the task to complete.

            Console.WriteLine("7 on thread {0}", Thread.CurrentThread.ManagedThreadId);
        }

        private static async Task<int> GetTotalAsync()
        {
            Console.WriteLine("2 on thread {0}", Thread.CurrentThread.ManagedThreadId);
            
            Task<int> task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("4 or 5 on thread {0}", Thread.CurrentThread.ManagedThreadId);
                Task.Delay(1000);
                return 100;
            });

            Console.WriteLine("3 on thread {0}", Thread.CurrentThread.ManagedThreadId);

            int totalRecord = await task; // Yield to the caller.

            Console.WriteLine("6 on thread {0}", Thread.CurrentThread.ManagedThreadId);

            return 100 + totalRecord;
        }
    }
}
