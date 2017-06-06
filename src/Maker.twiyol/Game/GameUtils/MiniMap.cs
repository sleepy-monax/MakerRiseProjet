using Maker.RiseEngine;
using Maker.RiseEngine.Rendering;
using Maker.Twiyol.Game;
using System.Drawing;

namespace Maker.Twiyol.Game.GameUtils
{
    public class MiniMap
    {

        GameScene G;

        public MiniMap(GameScene _WorldScene)
        {
            G = _WorldScene;
        }

        public Microsoft.Xna.Framework.Graphics.Texture2D MiniMapTexture2D;

        public void RefreshMiniMap()
        {

            MiniMapTexture2D = BitmapHelper.BitmapToTexture2D(Rise.Engine.GraphicsDevice, G.World.WorldBitmap);

        }
    }
}
