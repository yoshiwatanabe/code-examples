namespace MainProgram
{
    using System;
    using System.Collections.Generic;
    using Shapes;

    class Utility
    {
        static public void functionWithTwoArgs(object key, object value)
        {
            Console.WriteLine("Arg: {0}, Arg2: {1}", key, value);
        }

        static public void functionWithSingleArg(Tuple<object, object> keyValue)
        {
            Console.WriteLine("Key: {0}, Value: {1}", keyValue.Item1, keyValue.Item2);
        }

        static public void functionWithTwoArgsTyped(object key, float value)
        {
            Console.WriteLine("Arg: {0}, Arg2: {1}", key, value);
        }
    }

    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 1)
            {
                int intValue = 0;
                if (Int32.TryParse(args[0], out intValue))
                {
                    Console.WriteLine("An integer argument is specified {0}", intValue);
                }
                else
                {
                    Console.WriteLine("A non-integer argument is specified {0}", args[0]);
                }
            }
            else
            {
                Console.WriteLine("No argument is specified");
            }

            Utility.functionWithTwoArgs("Pi", 3.14);
            Utility.functionWithSingleArg(new Tuple<object, object>("Pi", 3.14));
            Utility.functionWithTwoArgsTyped("Pi", 3.14f);

            // for loop
            int n = 10;
            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine(i.ToString());
            }
            for (int i = n; i >= 0; i--)
            {
                Console.WriteLine(i.ToString());
            }

            // foreach array
            var states = new string[] { "WA", "OR", "CA" };
            foreach (var state in states)
            {
                Console.WriteLine(state);
            }

            // foreach list
            var stateList = new List<string>() {"WA", "OR", "CA" }.AsReadOnly();
            foreach (var state in stateList)
            {
                Console.WriteLine(state);
            }

            // while loop
            int counter = 10;
            while (counter < 20)
            {
                Console.WriteLine("value of counter: {0}", counter);
                counter = counter + 1;
            }

            // object instance
            var r = new Rectangle(2, 3);
            Console.WriteLine("The width is {0}. The area is {1}", r.Width, r.BoundingArea);
            r.Print();

            var c1 = new Circle();
            Console.WriteLine("The width is {0}", c1.Width);
            var c2 = new Circle(2);
            Console.WriteLine("The width is {0}", c2.Width);

            c2.Radius = 3;
            Console.WriteLine("The width is {0}", c2.Width);

            return 0;
        }
    }
}
