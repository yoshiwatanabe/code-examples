
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Sample10
    {
        public static void Demo()
        {
            BadParent();
            GoodParent();         
        }

        public static void BadParent()
        {
            Task parent = Task.Factory.StartNew(() =>
            {
                Task taskA = Task.Factory.StartNew(() => SomeLengthyOperation());
                Task taskB = Task.Factory.StartNew(() => SomeLengthyOperation());
            });

            parent.Wait();
            Console.WriteLine("Done waiting for bad parent who doesn't wair for children");

            // Give other threads chance to finish and print out.
            for (int i = 0; i < 2; i++)
            {
                Thread.Yield();
                Thread.Sleep(2000);
            }
        }

        private static void GoodParent()
        {
            Task parent = Task.Factory.StartNew(() =>
            {
                Task taskA = Task.Factory.StartNew(() => SomeLengthyOperation(), TaskCreationOptions.AttachedToParent);
                Task taskB = Task.Factory.StartNew(() => SomeLengthyOperation(), TaskCreationOptions.AttachedToParent);
            });

            parent.Wait();
            Console.WriteLine("Done waiting for good parent, who implicitly wait for children");
        }

        private static void SomeLengthyOperation()
        {
            Console.WriteLine("SomeLengthyOperation Begin. Thread ID : {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.SpinWait(5000000);
            Console.WriteLine("SomeLengthyOperation End. Thread ID : {0}", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
