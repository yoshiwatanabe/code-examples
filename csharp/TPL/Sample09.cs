using System;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Sample09
    {
        public static void Demo()
        {
            FirstWay();
            SecondWay();
        }

        private static void FirstWay()
        {
            // (5 + 4) * 9
            Task<int> t1 = Task<int>.Factory.StartNew(() => { return 5; });
            Task<int> t2 = t1.ContinueWith<int>((t) => { return t.Result + 4; });
            Task<int> t3 = t2.ContinueWith<int>((t) => { return t.Result * 9; });

            Console.WriteLine("Task result: {0}", t3.Result);
        }

        private static void SecondWay()
        {
            // (5 + 4) * 9
            Task<int> task = Task<int>.Factory.StartNew(() => { return 5; }).
                ContinueWith<int>((t) => { return t.Result + 4; }).
                    ContinueWith<int>((t) => { return t.Result * 9; });

            Console.WriteLine("Task result: {0}", task.Result);
        }
    }
}
