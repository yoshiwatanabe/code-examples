using System;

namespace Shapes
{
    public abstract class Shape
    {
        abstract public int Width { get; }
        abstract public int Height { get; }
        public int BoundingArea { get { return Width * Height; } }
        public virtual void Print() { DefautPrint(); }
        public void DefautPrint()
        {
            Console.WriteLine("I'm a shape");
        }
    }

    public class Rectangle : Shape
    {
        public Rectangle(int x, int y)
            : base()
        {
            this.Width = x;
            this.Height = y;
        }

        public override int Height { get; }
        public override int Width { get; }
        public override void Print()
        {
            Console.WriteLine("I'm a Rectangle");
        }
    }

    public class Circle : Shape
    {
        private int radius;
        public Circle(int rad)
        {
            radius = rad;
        }

        public override int Width { get { return radius * 2; } }
        public override int Height { get { return radius * 2; } }

        public Circle()
        {
            radius = 10;
        }

        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }
    }
}
