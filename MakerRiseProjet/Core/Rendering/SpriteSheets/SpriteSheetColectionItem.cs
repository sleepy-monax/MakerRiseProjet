using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiseEngine.Core.Rendering.SpriteSheets
{
    public class TilesheetColectionItem
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public bool Animated = false;
        public string[] Frames;
        public AnimationMode AnimMode;
        public int FrameTime = 0;

        public TilesheetColectionItem(int x, int y, int w, int h)
        {

            X = x;
            Y = y;
            Width = w;
            Height = h;

        }

        public TilesheetColectionItem(string[] f, AnimationMode m, int _FrameTime)
        {

            Animated = true;
            Frames = f;
            AnimMode = m;
            FrameTime = _FrameTime;

        }
    }

    public enum AnimationMode
    {
        Forward = 0,
        BackAndForward = 1
    }
}
