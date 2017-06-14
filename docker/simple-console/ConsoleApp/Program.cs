using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world");
            Console.WriteLine((args.Any() ? 
                String.Join(Environment.NewLine, args) : "<no argument>"));
        }
    }
}
