using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.World.Utils
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

            Zoom = 64;
            Size = new Point(Common.graphics.PreferredBackBufferWidth, Common.graphics.PreferredBackBufferHeight);

            FocusLocation = Point.Zero;
            PreciseFocusLocation = Vector2.Zero;

            StartTile = Point.Zero;
            EndTile = Point.Zero;

            ScreenOrigine = Point.Zero;

        }

        int DSx; //DrawStartX
        int DSy;

        int Sx;
        int Sy;


        int DEx;
        int DEy;


        int Ex;
        int Ey;

        int Dx;
        int Dy;


        int Ox;
        int Oy;

        public void Update()
        {

            DSx = FocusLocation.X - Config.Gfx.ViewDistance;
            DSy = FocusLocation.Y - Config.Gfx.ViewDistance;

            Sx = FocusLocation.X - Config.Gfx.ViewDistance;
            Sy = FocusLocation.Y - Config.Gfx.ViewDistance;

            if (DSx <= 0)
            {
                DSx = 0;

            }
            if (DSy <= 0)
            {
                DSy = 0;

            }

            StartTile = new Point(Sx, Sy);

            DEx = FocusLocation.X + Config.Gfx.ViewDistance;
            DEy = FocusLocation.Y + Config.Gfx.ViewDistance;

            Ex = FocusLocation.X + Config.Gfx.ViewDistance;
            Ey = FocusLocation.Y + Config.Gfx.ViewDistance;

            if (DEx > (W.worldProperty.Size * 16) - 1) DEx = (W.worldProperty.Size * 16) - 1;
            if (DEy > (W.worldProperty.Size * 16) - 1) DEy = (W.worldProperty.Size * 16) - 1;

            EndTile = new Point(Ex, Ey);

            Dx = Ex - Sx;
            Dy = Ey - Sy;

            Ox = (int)((Size.X / 2) - (Dx * Zoom) / 2 - (PreciseFocusLocation.X * Zoom));
            Oy = (int)((Size.Y / 2) - (Dy * Zoom) / 2 - (PreciseFocusLocation.Y * Zoom));

            ScreenOrigine = new Point(Ox, Oy);

        }
    }
}
