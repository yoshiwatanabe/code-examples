
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Sample11
    {
        public static void Demo()
        {
            Task<int>[] tasks = new Task<int>[] {
                Task<int>.Factory.StartNew(() => Multiply(5, 3)),
                Task<int>.Factory.StartNew(() => MultiplySlow(5, 3))
            };

            int taskIndx = Task.WaitAny(tasks);
            Console.WriteLine("Task at [{0}] produced the result {1}", taskIndx, tasks[taskIndx].Result);
        }

        private static int Multiply(int x, int y)
        {
            Console.WriteLine("Multiply {0} and {1}. Thread ID : {2}", x, y, Thread.CurrentThread.ManagedThreadId);            
            return x * y;
        }

        private static int MultiplySlow(int x, int y)
        {
            Console.WriteLine("MultiplySlow {0} and {1}. Thread ID : {2}", x, y, Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(5000);
            return x * y;
        }
    }
}
