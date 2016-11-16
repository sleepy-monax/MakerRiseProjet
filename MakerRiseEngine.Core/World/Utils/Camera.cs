using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.World.Utils
{
    public class GameCamera
    {

        WorldScene W;

        public int Zoom;
        public Point Size;

        public Vector2 PreciseFocusLocation;
        public Point FocusLocation;

        public Point ScreenOrigine; //Draw Orgine

        public Point StartTile;
        public Point EndTile;




        public GameCamera(WorldScene _WorldScene)
        {

            W = _WorldScene;

            Zoom = 8;
            Size = new Point(Common.graphics.PreferredBackBufferWidth, Common.graphics.PreferredBackBufferHeight);

            FocusLocation = Point.Zero;
            PreciseFocusLocation = Vector2.Zero;

            StartTile = Point.Zero;
            EndTile = Point.Zero;

            ScreenOrigine = Point.Zero;

        }

        int DrawStartX; //DrawStartX
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

            DrawStartX = FocusLocation.X - Config.Gfx.ViewDistance;
            DrawStartY = FocusLocation.Y - Config.Gfx.ViewDistance;

            StartX = FocusLocation.X - Config.Gfx.ViewDistance;
            StartY = FocusLocation.Y - Config.Gfx.ViewDistance;

            if (DrawStartX <= 0)
            {
                DrawStartX = 0;

            }
            if (DrawStartY <= 0)
            {
                DrawStartY = 0;

            }

            StartTile = new Point(StartX, StartY);

            DrawEndX = FocusLocation.X + Config.Gfx.ViewDistance;
            DrawEndY = FocusLocation.Y + Config.Gfx.ViewDistance;

            EndX = FocusLocation.X + Config.Gfx.ViewDistance;
            EndY = FocusLocation.Y + Config.Gfx.ViewDistance;

            if (DrawEndX > (W.worldProperty.Size * 16) - 1) DrawEndX = (W.worldProperty.Size * 16) - 1;
            if (DrawEndY > (W.worldProperty.Size * 16) - 1) DrawEndY = (W.worldProperty.Size * 16) - 1;

            EndTile = new Point(EndX, EndY);

            DeltaX = EndX - StartX;
            DeltaY = EndY - StartY;

            OrigineX = (int)((Size.X / 2) - (DeltaX * Zoom) / 2 - (PreciseFocusLocation.X * Zoom));
            OrigineY = (int)((Size.Y / 2) - (DeltaY * Zoom) / 2 - (PreciseFocusLocation.Y * Zoom));

            ScreenOrigine = new Point(OrigineX - Zoom / 2, OrigineY - Zoom / 2);


        }
    }
}
