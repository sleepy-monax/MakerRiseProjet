using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using Maker.RiseEngine.Core.UI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static Maker.RiseEngine.Core.Rendering.SpriteFontDraw;

namespace Maker.RiseEngine.Core.UI
{

    public enum Dock
    {
        UpLeft, Up, UpRight,
        Left, Center, Right,
        DownLeft, Down, DownRight
    }

    public class Container : Idrawable
    {

        public List<Control> Controls = new List<Control>();
        public Rectangle ContainerDefaultRect;
        public Rectangle ContainerRect;
        Point DockPoint;
        public bool ShowTitle = false;
        public string Title = "";

        public bool Visible = false;
        public Color BackColor;
        Dock ScreenDock;

        Sprite BoxC;
        Sprite BoxUL;
        Sprite BoxDL;
        Sprite BoxUR;
        Sprite BoxDR;
        Sprite BoxMU;
        Sprite BoxMD;
        Sprite BoxML;
        Sprite BoxMR;

        Sprite BoxTUL;
        Sprite BoxTUC;
        Sprite BoxTUR;


        public Container(int x, int y, int Width, int Height, bool visible, Dock screenDock, Color backColor)
        {
            ContainerRect = new Rectangle(x, y, Width, Height);
            ContainerDefaultRect = ContainerRect;
            Visible = visible;
            BackColor = backColor;
            ScreenDock = screenDock;
            BoxC = CommonSheets.GUI.GetSprite("BoxC");

            BoxUL = CommonSheets.GUI.GetSprite("BoxUL");
            BoxDL = CommonSheets.GUI.GetSprite("BoxDL");
            BoxUR = CommonSheets.GUI.GetSprite("BoxUR");
            BoxDR = CommonSheets.GUI.GetSprite("BoxDR");
            BoxMU = CommonSheets.GUI.GetSprite("BoxMU");
            BoxMD = CommonSheets.GUI.GetSprite("BoxMD");
            BoxML = CommonSheets.GUI.GetSprite("BoxML");
            BoxMR = CommonSheets.GUI.GetSprite("BoxMR");

            BoxTUC = CommonSheets.GUI.GetSprite("BoxTUC");
            BoxTUL = CommonSheets.GUI.GetSprite("BoxTUL");
            BoxTUR = CommonSheets.GUI.GetSprite("BoxTUR");
               
                

        }
        public Container(Rectangle ContainerLocation, bool visible, Dock screenDock, Color backColor)
        {
            ContainerRect = ContainerLocation;
            ContainerDefaultRect = ContainerRect;
            Visible = visible;
            BackColor = backColor;
            ScreenDock = screenDock;
            BoxC = CommonSheets.GUI.GetSprite("BoxC");

            BoxUL = CommonSheets.GUI.GetSprite("BoxUL");
            BoxDL = CommonSheets.GUI.GetSprite("BoxDL");
            BoxUR = CommonSheets.GUI.GetSprite("BoxUR");
            BoxDR = CommonSheets.GUI.GetSprite("BoxDR");
            BoxMU = CommonSheets.GUI.GetSprite("BoxMU");
            BoxMD = CommonSheets.GUI.GetSprite("BoxMD");
            BoxML = CommonSheets.GUI.GetSprite("BoxML");
            BoxMR = CommonSheets.GUI.GetSprite("BoxMR");

            BoxTUC = CommonSheets.GUI.GetSprite("BoxTUC");
            BoxTUL = CommonSheets.GUI.GetSprite("BoxTUL");
            BoxTUR = CommonSheets.GUI.GetSprite("BoxTUR");
        }

        public void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {

            switch (ScreenDock)
            {
                case Dock.Up:
                    DockPoint = new Point(Engine.graphics.PreferredBackBufferWidth / 2, 0);
                    break;
                case Dock.Center:
                    DockPoint = new Point(Engine.graphics.PreferredBackBufferWidth / 2, Engine.graphics.PreferredBackBufferHeight / 2);
                    break;
                case Dock.Down:
                    DockPoint = new Point(Engine.graphics.PreferredBackBufferWidth / 2, Engine.graphics.PreferredBackBufferHeight);
                    break;
                case Dock.UpLeft:
                    DockPoint = new Point(0, 0);
                    break;
                case Dock.Left:
                    DockPoint = new Point(0, Engine.graphics.PreferredBackBufferHeight / 2);
                    break;
                case Dock.DownLeft:
                    DockPoint = new Point(0, Engine.graphics.PreferredBackBufferHeight);
                    break;
                case Dock.UpRight:
                    DockPoint = new Point(Engine.graphics.PreferredBackBufferWidth, 0);
                    break;
                case Dock.Right:
                    DockPoint = new Point(Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight / 2);
                    break;
                case Dock.DownRight:
                    DockPoint = new Point(Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight);
                    break;
                default:
                    break;
            }

            ContainerRect = new Rectangle(ContainerDefaultRect.Location.X + DockPoint.X, ContainerDefaultRect.Location.Y + DockPoint.Y, ContainerDefaultRect.Width, ContainerDefaultRect.Height);

            foreach (Control ctrl in this.Controls) ctrl.Update(Mouse, KeyBoard, gameTime, this.ContainerRect.X, this.ContainerRect.Y);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            if (Visible == true)
            {
                //Drawing Corner
                BoxUL.Draw(spriteBatch, new Rectangle(ContainerRect.Location.X, ContainerRect.Location.Y, 64, 64), BackColor, gameTime);
                BoxDL.Draw(spriteBatch, new Rectangle(ContainerRect.Location.X, ContainerRect.Location.Y + ContainerRect.Height - 64, 64, 64), BackColor, gameTime);
                BoxUR.Draw(spriteBatch, new Rectangle(ContainerRect.Location.X + ContainerRect.Width - 64, ContainerRect.Location.Y, 64, 64), BackColor, gameTime);
                BoxDR.Draw(spriteBatch, new Rectangle(ContainerRect.Location.X + ContainerRect.Width - 64, ContainerRect.Location.Y + ContainerRect.Height - 64, 64, 64), BackColor, gameTime);



                if (ContainerRect.Width > 128)
                {

                    BoxMU.Draw(spriteBatch, new Rectangle(ContainerRect.Location.X + 64, ContainerRect.Location.Y, ContainerRect.Width - 128, 64), BackColor, gameTime);
                    BoxMD.Draw(spriteBatch, new Rectangle(ContainerRect.Location.X + 64, ContainerRect.Location.Y + ContainerRect.Height - 64, ContainerRect.Width - 128, 64), BackColor, gameTime);
                }
                if (ContainerRect.Height > 128)
                {
                    BoxML.Draw(spriteBatch, new Rectangle(ContainerRect.Location.X, ContainerRect.Location.Y + 64, 64, ContainerRect.Height - 128), BackColor, gameTime);
                    BoxMR.Draw(spriteBatch, new Rectangle(ContainerRect.Location.X + ContainerRect.Width - 64, ContainerRect.Location.Y + 64, 64, ContainerRect.Height - 128), BackColor, gameTime);
                }
                if (ContainerRect.Height > 128)
                {
                    if (ContainerRect.Width > 128)
                    {
                        BoxC.Draw(spriteBatch, new Rectangle(ContainerRect.Location.X + 64, ContainerRect.Location.Y + 64, ContainerRect.Width - 128, ContainerRect.Height - 128), BackColor, gameTime);
                    }
                }

                
            }

            if (ShowTitle)
            {

                BoxTUL.Draw(spriteBatch, new Rectangle(ContainerRect.Location.X, ContainerRect.Location.Y, 64, 64), BackColor, gameTime);
                BoxTUR.Draw(spriteBatch, new Rectangle(ContainerRect.Location.X + ContainerRect.Width - 64, ContainerRect.Location.Y, 64, 64), BackColor, gameTime);
                BoxTUC.Draw(spriteBatch, new Rectangle(ContainerRect.Location.X + 64, ContainerRect.Location.Y, ContainerRect.Width - 128, 64), BackColor, gameTime);

                spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "segoeUI_16pt"), Title, new Rectangle(ContainerRect.Location.X, ContainerRect.Location.Y, ContainerRect.Width, 64), Alignment.Center, Style.DropShadow, Color.White);
            }

            foreach (Control i in this.Controls)
            {
                i.Draw(spriteBatch, gameTime, this.ContainerRect.X, this.ContainerRect.Y);
                if (Engine.engineConfig.Debug_GuiFrame)
                {
                    spriteBatch.DrawRectangle(i.ClickRect, Color.Black);
                }
            }
            if (Engine.engineConfig.Debug_GuiFrame) spriteBatch.DrawRectangle(ContainerRect, Color.Black);
        }

        public void applyLayout(string name) {

        }
    }
}
