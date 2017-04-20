using System;

namespace NewFeature
{
    class OutVariableIgnoreDemo
    {
        public static void Run()
        {
            if (int.TryParse("256", out _))
            {
                Console.WriteLine("It was an integer");
            }
        }
    }
}
