
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Sample04
    {
        public static void Demo()
        {
            Task taskA = Task.Run(() => Console.WriteLine("TaskA. Thread ID : {0}", Thread.CurrentThread.ManagedThreadId));
            Task taskB = Task.Run(() => Console.WriteLine("TaskB. Thread ID : {0}", Thread.CurrentThread.ManagedThreadId));
            
            Console.WriteLine("TaskMain. Thread ID : {0}", Thread.CurrentThread.ManagedThreadId);
            Task.WaitAll(taskA, taskB);
        }
    }
}
