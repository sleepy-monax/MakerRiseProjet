using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Maker.Skift.Controls
{
    public enum MouseState : byte
    {
        None = 0,
        Over = 1,
        Down = 2,
        Block = 3
    }

    public static class Helpers
    {

        // r,g,b values are from 0 to 1
        // h = [0,360], s = [0,1], v = [0,1]
        //              if s == 0, then h = -1 (undefined)
        /// <summary>
        /// Generates the Hue, Saturation, and Value for a given color
        /// </summary>
        /// <param name="color">The Color to generate the values for</param>
        /// <param name="hue">Out value for Hue</param>
        /// <param name="saturation">Out value for Saturation</param>
        /// <param name="value">Out value for Value</param>
        public static void HSVFromRGB(Color color, out double hue, out double saturation, out double value)
        {
            double min, max, delta, r, g, b;
            r = (double)color.R / 255d;
            g = (double)color.G / 255d;
            b = (double)color.B / 255d;

            min = Math.Min(r, Math.Min(g, b));
            max = Math.Max(r, Math.Max(g, b));
            value = max;                               // v
            delta = max - min;
            if (max != 0)
                saturation = delta / max;               // s
            else
            {
                // r = g = b = 0                // s = 0, v is undefined
                saturation = 0;
                hue = -1;
                return;
            }
            if (r == max)
                hue = (g - b) / delta;         // between yellow & magenta
            else if (g == max)
                hue = 2 + (b - r) / delta;     // between cyan & yellow
            else
                hue = 4 + (r - g) / delta;     // between magenta & cyan
            hue *= 60;                               // degrees
            if (hue < 0)
                hue += 360;
        }


        /// <summary>
        /// Generates a Color from a Hue, Saturation, and Value combination
        /// </summary>
        /// <param name="hue">Hue to use for the Color. (Max 360)</param>
        /// <param name="saturation">Saturation to use for the Color. (Max 1.0)</param>
        /// <param name="value">Value to use for the Color. (Max 1.0)</param>
        /// <returns>Generated Color</returns>
        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            try
            {
                int i;
                double f, p, q, t, r, g, b;

                if (saturation == 0)
                {
                    // achromatic (grey)
                    r = g = b = value;
                    return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));
                }
                hue /= 60;                        // sector 0 to 5
                i = (int)Math.Floor(hue);
                f = hue - i;                      // factorial part of h
                p = value * (1 - saturation);
                q = value * (1 - saturation * f);
                t = value * (1 - saturation * (1 - f));
                switch (i)
                {
                    case 0:
                        r = value;
                        g = t;
                        b = p;
                        break;
                    case 1:
                        r = q;
                        g = value;
                        b = p;
                        break;
                    case 2:
                        r = p;
                        g = value;
                        b = t;
                        break;
                    case 3:
                        r = p;
                        g = q;
                        b = value;
                        break;
                    case 4:
                        r = t;
                        g = p;
                        b = value;
                        break;
                    default:                // case 5:
                        r = value;
                        g = p;
                        b = q;
                        break;
                }
                return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));
            }
            catch
            {

            }
            return Color.Empty;
        }

        public static readonly StringFormat NearSF = new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Near
        };

        public static readonly StringFormat CenterSF = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        public static GraphicsPath RoundRec(Rectangle Rectangle, int Curve)
        {
            GraphicsPath P = new GraphicsPath();
            int ArcRectangleWidth = Curve * 2;
            P.AddArc(new Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90);
            P.AddArc(new Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90);
            P.AddArc(new Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90);
            P.AddArc(new Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90);
            P.AddLine(new Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), new Point(Rectangle.X, Curve + Rectangle.Y));
            return P;
        }

        public static GraphicsPath RoundRect(float x, float y, float w, float h, double r = 0.3,
            bool TL = true, bool TR = true, bool BR = true, bool BL = true)
        {
            GraphicsPath functionReturnValue = null;
            float d = Math.Min(w, h) * (float)r;
            float xw = x + w;
            float yh = y + h;
            functionReturnValue = new GraphicsPath();

            var _with1 = functionReturnValue;
            if (TL)
                _with1.AddArc(x, y, d, d, 180, 90);
            else
                _with1.AddLine(x, y, x, y);
            if (TR)
                _with1.AddArc(xw - d, y, d, d, 270, 90);
            else
                _with1.AddLine(xw, y, xw, y);
            if (BR)
                _with1.AddArc(xw - d, yh - d, d, d, 0, 90);
            else
                _with1.AddLine(xw, yh, xw, yh);
            if (BL)
                _with1.AddArc(x, yh - d, d, d, 90, 90);
            else
                _with1.AddLine(x, yh, x, yh);

            _with1.CloseFigure();
            return functionReturnValue;
        }

        //-- Credit: AeonHack
        public static GraphicsPath DrawArrow(int x, int y, bool flip)
        {
            GraphicsPath GP = new GraphicsPath();

            int W = 12;
            int H = 6;

            if (flip)
            {
                GP.AddLine(x + 1, y, x + W + 1, y);
                GP.AddLine(x + W, y, x + H, y + H - 1);
            }
            else
            {
                GP.AddLine(x, y + H, x + W, y + H);
                GP.AddLine(x + W, y + H, x + H, y);
            }

            GP.CloseFigure();
            return GP;
        }

        public static void drawShadow(Graphics G, Color c, Color c2, GraphicsPath GP, int d)
        {
            Color[] colors = getColorVector(c, c2, d).ToArray();
            for (int i = 0; i < d; i++)
            {
                G.TranslateTransform(1f, 0.75f);                // <== shadow vector!
                using (Pen pen = new Pen(colors[i], 1.75f))  // <== pen width (*)
                    G.DrawPath(pen, GP);
            }
            G.ResetTransform();
        }


        public static List<Color> getColorVector(Color fc, Color bc, int depth)
        {
            List<Color> cv = new List<Color>();
            float dRed = 1f * (bc.R - fc.R) / depth;
            float dGreen = 1f * (bc.G - fc.G) / depth;
            float dBlue = 1f * (bc.B - fc.B) / depth;
            float dAlpha = 1f * (bc.A - fc.B) / depth;
            for (int d = 1; d <= depth; d++)
                cv.Add(Color.FromArgb((int)(fc.A + dAlpha *d), (int)(fc.R + dRed * d),
                  (int)(fc.G + dGreen * d), (int)(fc.B + dBlue * d)));
            return cv;
        }

        public static GraphicsPath getRectPath(Rectangle R)
        {
            byte[] fm = new byte[3];
            for (int b = 0; b < 3; b++) fm[b] = 1;
            List<Point> points = new List<Point>();
            points.Add(new Point(R.Left, R.Bottom));
            points.Add(new Point(R.Right, R.Bottom));
            points.Add(new Point(R.Right, R.Top));
            return new GraphicsPath(points.ToArray(), fm);
        }

    }

}
