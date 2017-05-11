using Maker.RiseEngine.Core.Ressources;

namespace Maker.RiseEngine.Core.Rendering.SpriteSheets
{
    public static class CommonSheets
    {

        //Gardé pour compatibilité avec l'encien code.

        public static SpriteSheet GUI;
        public static SpriteSheet Map;
        public static SpriteSheet Cursor;

        public static void Load(RessourcesManager RESSOUCES)
        {
            GUI = new SpriteSheet("Engine", RESSOUCES.GetTexture2D("Engine", "Tilesheet_GUI"), "Tilesheet_GUI", new Microsoft.Xna.Framework.Point(64));
            Map = new SpriteSheet("Engine", RESSOUCES.GetTexture2D("Engine", "Tilesheet_MapIcon"), "Tilesheet_MapIcon", new Microsoft.Xna.Framework.Point(16));
            Cursor = new SpriteSheet("Engine", RESSOUCES.GetTexture2D("Engine", "Tilesheet_Cursor"), "Tilesheet_Cursor", new Microsoft.Xna.Framework.Point(32));
        }

    }
}
