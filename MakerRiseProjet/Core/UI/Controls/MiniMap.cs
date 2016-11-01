using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RiseEngine.Core.Rendering.SpriteSheets;
using RiseEngine.Core.World.Utils;
using RiseEngine.Core.Rendering;

namespace RiseEngine.Core.UI.Controls
{
    public class MiniMap : UI.Control
    {

        World.WorldScene W;
        Sprite MiniMapSprite = CommonSheets.GUI.GetSprite("MiniMap");
        Sprite IconHouse = CommonSheets.Map.GetSprite("House");
        Sprite IconDot = CommonSheets.Map.GetSprite("Dot");

        Rectangle Focus;

        public MiniMap(int x , int y,World.WorldScene _World) {

            W = _World;
            Focus = new Rectangle(0,0,128,128);
            this.SizeBox = new Rectangle(new Point(x, y), new Point(192, 192));

        }


        public override void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime, int ContainerX, int ContainerY)
        {


            Focus = new Rectangle(W.Camera.FocusLocation.X - 89, W.Camera.FocusLocation.Y - 89, 178, 178);

            if (Focus.Location.X + Focus.Size.X > W.miniMap.MiniMapTexture2D.Width) Focus.X = W.miniMap.MiniMapTexture2D.Width - 178;
            if (Focus.Location.Y + Focus.Size.Y > W.miniMap.MiniMapTexture2D.Height) Focus.Y = W.miniMap.MiniMapTexture2D.Height - 178;

            if (Focus.Location.X < 0) Focus.X = 0;
            if (Focus.Location.Y < 0) Focus.Y = 0;

            base.Update(Mouse, KeyBoard, gameTime, ContainerX, ContainerY);
        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, int x, int y)
        {
            spriteBatch.Draw(W.miniMap.MiniMapTexture2D, new Rectangle(7 + SizeBox.Location.X + x, 7 + SizeBox.Location.Y + y, 178, 178), Focus, Color.White);
            MiniMapSprite.Draw(spriteBatch, new Rectangle(0 + SizeBox.Location.X + x, 0 + SizeBox.Location.Y + y, 192, 192), Color.White, gameTime);

            foreach (KeyValuePair<int, World.WorldObj.ObjRegion> Rg in W.Region) {

                if (Focus.Contains(Location.ToPoint(Rg.Value.Origine)))
                IconHouse.Draw(spriteBatch, new Rectangle(Location.ToPoint(Rg.Value.Origine).X - Focus.Location.X + SizeBox.Location.X + x, Location.ToPoint(Rg.Value.Origine).Y - Focus.Location.Y + SizeBox.Location.Y + y, 16,16), Rg.Value.Color, gameTime);

            }

            IconDot.Draw(spriteBatch, new Rectangle(W.Camera.FocusLocation.X - Focus.Location.X + SizeBox.Location.X + x, W.Camera.FocusLocation.Y - Focus.Location.Y + SizeBox.Location.Y + y, 16, 16), Color.DarkRed, gameTime);

            base.Draw(spriteBatch, gameTime, x,y);
        }

    }
}
