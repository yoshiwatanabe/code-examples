using System;

namespace NewFeature
{
    class OutVariableDemo
    {
        public static void Run()
        {
            if (int.TryParse("256", out int number))
            {
                Console.WriteLine($"{nameof(number)} is {number}");
            }
        }
    }
}
