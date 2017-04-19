
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Sample05
    {
        public static void Demo()
        {
            Task taskA = Task.Factory.StartNew(() => Console.WriteLine("TaskA. Thread ID : {0}", Thread.CurrentThread.ManagedThreadId));
            Task taskB = Task.Factory.StartNew(() => Console.WriteLine("TaskB. Thread ID : {0}", Thread.CurrentThread.ManagedThreadId));
            
            Console.WriteLine("TaskMain. Thread ID : {0}", Thread.CurrentThread.ManagedThreadId);
            Task.WaitAll(taskA, taskB);
        }
    }
}
