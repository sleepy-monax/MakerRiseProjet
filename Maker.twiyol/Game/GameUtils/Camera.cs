using Maker.RiseEngine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.twiyol.Game.GameUtils
{
    public class GameCamera
    {

        GameScene G;

        public int TileUnit;
        public Point Size;

        public Vector2 PreciseFocusLocation;

        public Point ScreenOrigine; //Draw Orgine

        public Point StartTile;
        public Point EndTile;

        public GameCamera(GameScene _WorldScene)
        {

            G = _WorldScene;

            TileUnit = 64;
            Size = new Point(Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight);

            PreciseFocusLocation = Vector2.Zero;

            StartTile = Point.Zero;
            EndTile = Point.Zero;

            ScreenOrigine = Point.Zero;

        }

        int DrawStartX;
        int DrawStartY;

        int StartX;
        int StartY;


        int DrawEndX;
        int DrawEndY;


        int EndX;
        int EndY;

        int DeltaX;
        int DeltaY;


        int OrigineX;
        int OrigineY;

        public void Update()
        {
            Point FocusLocation = G.World.Camera.FocusLocation.ToPoint();

            Size = new Point(Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight);
            DrawStartX = FocusLocation.X - Engine.engineConfig.GFX_ViewDistance;
            DrawStartY = FocusLocation.Y - Engine.engineConfig.GFX_ViewDistance;

            StartX = FocusLocation.X - Engine.engineConfig.GFX_ViewDistance;
            StartY = FocusLocation.Y - Engine.engineConfig.GFX_ViewDistance;

            if (DrawStartX <= 0)
            {
                DrawStartX = 0;

            }
            if (DrawStartY <= 0)
            {
                DrawStartY = 0;

            }

            StartTile = new Point(StartX, StartY);

            DrawEndX = FocusLocation.X + Engine.engineConfig.GFX_ViewDistance;
            DrawEndY = FocusLocation.Y + Engine.engineConfig.GFX_ViewDistance;

            EndX = FocusLocation.X + Engine.engineConfig.GFX_ViewDistance;
            EndY = FocusLocation.Y + Engine.engineConfig.GFX_ViewDistance;

            if (DrawEndX > (G.World.Size * 16) - 1) DrawEndX = (G.World.Size * 16) - 1;
            if (DrawEndY > (G.World.Size * 16) - 1) DrawEndY = (G.World.Size * 16) - 1;

            EndTile = new Point(EndX, EndY);

            DeltaX = EndX - StartX;
            DeltaY = EndY - StartY;

            OrigineX = (int)((Size.X / 2) - (DeltaX * TileUnit) / 2 - (PreciseFocusLocation.X * TileUnit));
            OrigineY = (int)((Size.Y / 2) - (DeltaY * TileUnit) / 2 - (PreciseFocusLocation.Y * TileUnit));

            ScreenOrigine = new Point(OrigineX - TileUnit / 2, OrigineY - TileUnit / 2);


        }
    }
}
