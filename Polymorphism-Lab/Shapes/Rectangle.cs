using System;
using System.Collections.Generic;
using System.Text;

namespace Shapes
{
    public class Rectangle : Shape
    {
        private double height;
        private double width;

        public Rectangle(double height, double width)
        {
            Height = height;
            Width = width;
        }

        public double Height
        {
            get => height;
            private set => height = value;
        }

        public double Width
        {
            get => width;
            private set => width = value;
        }

        public override double CalculatePerimeter()
        {
            return (Height + Width) * 2;
        }

        public override double CalculateArea()
        {
            return Height * Width;
        }

        public override string Draw()
        {
            return base.Draw() + $"{this.GetType().Name}";
        }
    }
}
