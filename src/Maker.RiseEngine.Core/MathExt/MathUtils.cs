using Microsoft.Xna.Framework;
using System;

namespace Maker.RiseEngine.MathExt
{
    public static class MathUtils
    {

        public static double GetDistance(this Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
        }

        public static Rectangle CreateRectangleFromTwoPoint(Point p1, Point p2)
        {

            int x;
            int Width;
            if (p1.X < p2.X)
            {
                x = p1.X;
                Width = p2.X - p1.X;
            }
            else
            {
                x = p2.X;
                Width = p1.X - p2.X;
            }

            int y;
            int height;
            if (p1.Y < p2.Y)
            {
                y = p1.Y;
                height = p2.Y - p1.Y;
            }
            else
            {
                y = p2.Y;
                height = p1.Y - p2.Y;
            }

            return new Rectangle(x, y, Width, height);

        }

    }
}
