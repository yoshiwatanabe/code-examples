using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Sample01
    {
        public static void Demo()
        {
            Console.WriteLine("Main. Thread ID : {0}", Thread.CurrentThread.ManagedThreadId);
            Parallel.Invoke(
                () => { Console.WriteLine("Task 1. Thread ID : {0}", Thread.CurrentThread.ManagedThreadId); },
                () => { Console.WriteLine("Task 1. Thread ID : {0}", Thread.CurrentThread.ManagedThreadId); }
            );
        }
    }
}
