using System;

namespace NewFeature
{
    class TupleDeconstructingDemo
    {
        public (string First, string Last) LookupName(long id) // tuple return type
        {
            return ("John", "Doe"); // tuple literal
        }

        public static void Run()
        {
            var (FirstName, LastName) = (new TupleDeconstructingDemo()).LookupName(331);
            Console.WriteLine($"{nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}");
        }

        public static void RunOLDSTYLE()
        {
            var name = (new TupleDeconstructingDemo()).LookupName(331);
            var FirstName = name.First;
            var LastName = name.Last;
            Console.WriteLine($"{nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}");
        }

        // Best-practices & guidlines
        // https://www.infoq.com/articles/Patterns-Practices-CSharp-7
    }
}
