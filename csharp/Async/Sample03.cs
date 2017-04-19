using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncSamples
{
    class Sample03
    {
        public static void Demo()
        {
            Console.WriteLine("1 on thread {0}", Thread.CurrentThread.ManagedThreadId);

            Task<int> task = GetTotalAsync();

            Console.WriteLine("5 on thread {0}", Thread.CurrentThread.ManagedThreadId);

            int total = task.Result; // Blocks, give the task to complete.

            Console.WriteLine("8 on thread {0}", Thread.CurrentThread.ManagedThreadId);
        }

        private static async Task<int> GetTotalAsync()
        {
            Console.WriteLine("2 on thread {0}", Thread.CurrentThread.ManagedThreadId);

            Task<int> task = GetSubTotalAsync();

            Console.WriteLine("4 on thread {0}", Thread.CurrentThread.ManagedThreadId);

            int subTotal = await task; // Suspension point. Yield to the caller.

            Console.WriteLine("7 on thread {0}", Thread.CurrentThread.ManagedThreadId);

            return subTotal * 2;
        }

        private static async Task<int> GetSubTotalAsync()
        {
            Console.WriteLine("3 on thread {0}", Thread.CurrentThread.ManagedThreadId);

            await Task.Delay(1000); // Suspension point. Yield to the caller.

            Console.WriteLine("6 on thread {0}", Thread.CurrentThread.ManagedThreadId);

            return 150;
        }
    }
}
