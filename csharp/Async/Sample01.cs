using System;
using System.Threading.Tasks;

namespace AsyncSamples
{
    class Sample01
    {
        public static void Demo()
        {
            Console.WriteLine("1");

            Task<int> task = GetTotalAsync();

            Console.WriteLine("3");

            int count = task.Result; // Blocks until the task is done

            Console.WriteLine("5");
        }

        private static async Task<int> GetTotalAsync()
        {
            Console.WriteLine("2");

            await Task.Delay(2000); // Simulate some lengty work. Yield to caller

            Console.WriteLine("4");

            return 100; // We have a result. The task is done.   
        }

    }

}
