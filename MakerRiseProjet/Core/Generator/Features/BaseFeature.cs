using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.Generator.Features
{
    public class BaseFeature
    {

        public virtual void Apply(int[,] _Grid, Bitmap _Bitmap, World.WorldScene _World) { }

    }
}
