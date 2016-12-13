
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Sample08
    {
        public static void Demo()
        {
            Bad();
            Good();
        }

        public static void Bad()
        {
            DateTime begin = DateTime.UtcNow;

            Task[] tasks = new Task[10];
            for (int i = 0; i < tasks.Length; i++)
            {
                // 'i' is visible, but the value is always 10 for all ten tasks.
                tasks[i] = Task.Factory.StartNew(() => { Console.WriteLine("counter variable i is {0}", i); });
            }

            Task.WaitAll(tasks);

            Console.WriteLine("Measured time: " + (DateTime.UtcNow - begin).TotalMilliseconds + " ms.");
        }

        public static void Good()
        {
            DateTime begin = DateTime.UtcNow;

            Task[] tasks = new Task[10];
            for (int i = 0; i < tasks.Length; i++)
            {
                // 'i' is explicitly passed as an argument. This works.
                tasks[i] = Task.Factory.StartNew((Object state) => { Console.WriteLine("counter variable i is {0}", state); }, i);
            }

            Task.WaitAll(tasks);

            Console.WriteLine("Measured time: " + (DateTime.UtcNow - begin).TotalMilliseconds + " ms.");
        }
    }
}
