
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Sample03
    {
        public static void Demo()
        {
            Task taskA = new Task(() => Console.WriteLine("TaskA. Thread ID : {0}", Thread.CurrentThread.ManagedThreadId));
            Task taskB = new Task(() => Console.WriteLine("TaskB. Thread ID : {0}", Thread.CurrentThread.ManagedThreadId));
            taskA.Start();
            taskB.Start();
            
            Console.WriteLine("TaskMain. Thread ID : {0}", Thread.CurrentThread.ManagedThreadId);
            Task.WaitAll(taskA, taskB);
        }
    }
}
