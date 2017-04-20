using System;

namespace NewFeature
{
    class TupleReturnDemo
    {
        public Tuple<string, string> LookupNameOLDSTYLE(long id)
        {
            return Tuple.Create("John", "Doe");
        }

        public (string, string) LookupName(long id) // tuple return type
        {
            return ("John", "Doe"); // tuple literal
        }

        public static void Run()
        {
            var name = (new TupleReturnDemo()).LookupName(331);
            Console.WriteLine($"First: {name.Item1}, Last: {name.Item2}");
        }
    }
}
