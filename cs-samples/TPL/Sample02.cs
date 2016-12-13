using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Sample02
    {
        public static void Demo()
        {
            Task taskA = new Task(() => Console.WriteLine("TaskA. Thread ID : {0}", Thread.CurrentThread.ManagedThreadId));            
            taskA.Start();
            
            Console.WriteLine("TaskMain. Thread ID : {0}", Thread.CurrentThread.ManagedThreadId);
            taskA.Wait();
        }
    }
}
