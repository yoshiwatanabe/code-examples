using System;

namespace NewFeature
{
    class Rectangle
    {
        public int Width { get; set; } = 10;
        public int Height { get; set; } = 10;
        public string BackgrounColor { get; set; } = "White";
        public string BoarderColor { get; set; } = "Black";

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void Deconstruct(out int width, out int height)
        {
            width = Width;
            height = Height;
        }
    }

    class TupleDeconstructorDemo
    {
        public static void Run()
        {
            var rec = new Rectangle(100, 50);
            rec.Height *= 2;

            var (width, height) = rec;
            Console.WriteLine($"Width: {width}, Height: {height}");

        }        
    }
}
