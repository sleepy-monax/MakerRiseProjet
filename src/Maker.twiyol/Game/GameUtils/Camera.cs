using Maker.RiseEngine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Twiyol.Game.GameUtils
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
        public Point DeltaTile;

        public GameCamera(GameScene _WorldScene)
        {

            G = _WorldScene;

            TileUnit = 64;
            Size = new Point(Rise.Engine.graphicsDeviceManager.PreferredBackBufferWidth, Rise.Engine.graphicsDeviceManager.PreferredBackBufferHeight);

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

            Size = new Point(Rise.Engine.graphicsDeviceManager.PreferredBackBufferWidth, Rise.Engine.graphicsDeviceManager.PreferredBackBufferHeight);
            DrawStartX = FocusLocation.X - Rise.Engine.userConfig.GraphicsViewDistance;
            DrawStartY = FocusLocation.Y - Rise.Engine.userConfig.GraphicsViewDistance;

            StartX = FocusLocation.X - Rise.Engine.userConfig.GraphicsViewDistance;
            StartY = FocusLocation.Y - Rise.Engine.userConfig.GraphicsViewDistance;

            if (DrawStartX <= 0)
            {
                DrawStartX = 0;
            }
            if (DrawStartY <= 0)
            {
                DrawStartY = 0;

            }

            StartTile = new Point(StartX, StartY);

            DrawEndX = FocusLocation.X + Rise.Engine.userConfig.GraphicsViewDistance;
            DrawEndY = FocusLocation.Y + Rise.Engine.userConfig.GraphicsViewDistance;

            EndX = FocusLocation.X + Rise.Engine.userConfig.GraphicsViewDistance;
            EndY = FocusLocation.Y + Rise.Engine.userConfig.GraphicsViewDistance;

            if (DrawEndX > (G.World.Size * 16) - 1) DrawEndX = (G.World.Size * 16) - 1;
            if (DrawEndY > (G.World.Size * 16) - 1) DrawEndY = (G.World.Size * 16) - 1;

            EndTile = new Point(EndX, EndY);

            DeltaX = EndX - StartX;
            DeltaY = EndY - StartY;

            DeltaTile = new Point(DeltaX, DeltaY);

            OrigineX = (int)((Size.X / 2) - (DeltaX * TileUnit) / 2 - (PreciseFocusLocation.X * TileUnit));
            OrigineY = (int)((Size.Y / 2) - (DeltaY * TileUnit) / 2 - (PreciseFocusLocation.Y * TileUnit));

            ScreenOrigine = new Point(OrigineX - TileUnit / 2, OrigineY - TileUnit / 2);


        }
    }
}
