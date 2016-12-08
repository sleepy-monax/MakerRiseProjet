namespace Maker.RiseEngine.Core.Game.GameUtils
{
    public class MiniMap
    {

        GameScene G;

        public MiniMap(GameScene _WorldScene)
        {
            G = _WorldScene;
        }

        public System.Drawing.Bitmap MiniMapBitmap;
        public Microsoft.Xna.Framework.Graphics.Texture2D MiniMapTexture2D;

        public void RefreshMiniMap() {

            MiniMapTexture2D = Rendering.BitmapHelper.BitmapToTexture2D(Common.GraphicsDevice, MiniMapBitmap);

        }
    }
}
