using Maker.RiseEngine.Rendering.SpriteSheets;

using Microsoft.Xna.Framework;

namespace Maker.RiseEngine.Ressources
{
    public static class Common
    {
        public static SpriteSheet UserInterface;
        public static SpriteSheet MouseCursor;

        public static void LoadCommonRessource(RessourcesManager ressourceManager)
        {
            UserInterface = new SpriteSheet("Engine", ressourceManager.GetTexture2D("Engine", "Tilesheet_GUI"), "Tilesheet_GUI", new Point(64));
            MouseCursor = new SpriteSheet("Engine", ressourceManager.GetTexture2D("Engine", "Tilesheet_Cursor"), "Tilesheet_Cursor", new Point(32));
        }
    }
}
