using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Rendering;
using Maker.twiyol.Game;
using System.Drawing;

namespace Maker.twiyol.Game.GameUtils
{
    public class MiniMap
    {

        GameScene G;

        public MiniMap(GameScene _WorldScene)
        {
            G = _WorldScene;
        }

        public Bitmap MiniMapBitmap;
        public Microsoft.Xna.Framework.Graphics.Texture2D MiniMapTexture2D;

        public void RefreshMiniMap()
        {

            MiniMapTexture2D = BitmapHelper.BitmapToTexture2D(Engine.GraphicsDevice, MiniMapBitmap);

        }
    }
}
