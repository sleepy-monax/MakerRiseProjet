using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.World.Utils
{
    public class MiniMap
    {

        WorldScene W;

        public MiniMap(WorldScene _WorldScene)
        {
            W = _WorldScene;
        }

        public System.Drawing.Bitmap MiniMapBitmap;
        public Microsoft.Xna.Framework.Graphics.Texture2D MiniMapTexture2D;

        public void RefreshMiniMap() {

            MiniMapTexture2D = Rendering.Helper.BitmapToTexture2D(Common.GraphicsDevice, MiniMapBitmap);

        }
    }
}
