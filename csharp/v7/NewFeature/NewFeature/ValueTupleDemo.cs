using System;

namespace NewFeature
{
    class ValueTupleDemo
    {
        public (string First, string Last) LookupName(long id) // tuple return type
        {
            return ("John", "Doe"); // tuple literal
        }

        public static void Run()
        {
            var name = (new ValueTupleDemo()).LookupName(331);
            Console.WriteLine($"{nameof(name.First)}: {name.First}, {nameof(name.Last)}: {name.Last}");
        }
    }
}
