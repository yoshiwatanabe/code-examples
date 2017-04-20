using System;

namespace NewFeature
{
    class PatternMatchingSwitchDemo
    {
        class Shape { }
        class Circle : Shape { public double Radius; }
        class Rectangle : Shape { public int Length; public int Height; }

        public static void Run()
        {
            Shape shape = new Rectangle { Length = 10, Height = 5 };
            string line = null;
            switch (shape)
            {
                case Circle c:
                    line = $"circle with radius {c.Radius}";
                    break;
                case Rectangle s when (s.Length == s.Height):
                    line = $"{s.Length} x {s.Height} square";
                    break;
                case Rectangle r:
                    line = $"{r.Length} x {r.Height} rectangle";
                    break;
                default:
                    line = "<unknown shape>";
                    break;
                case null:
                    throw new ArgumentNullException(nameof(shape));
            }

            Console.WriteLine(line);
        }
    }
}
