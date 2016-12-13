
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Sample06
    {
        public static void Demo()
        {
            DateTime begin = DateTime.UtcNow;
            
            Task<int> taskA = Task<int>.Factory.StartNew(() => Multiply(5, 6));
            Task<int> taskB = Task<int>.Factory.StartNew(() => Multiply(10, 7));
            
            Task.WaitAll(taskA, taskB); // Blocks for about 5 seconds
            Console.WriteLine("Measured time: " + (DateTime.UtcNow - begin).TotalMilliseconds + " ms.");

            Console.WriteLine("TaskA result: {0}", taskA.Result); // Blocks, but the task is already done.
            Console.WriteLine("TaskB result: {0}", taskB.Result);

            Console.WriteLine("Measured time: " + (DateTime.UtcNow - begin).TotalMilliseconds + " ms.");
        }

        private static int Multiply(int x, int y)
        {
            Console.WriteLine("Multiply {0} and {1}. Thread ID : {2}", x, y, Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(5000);
            return x * y;
        }
    }
}
