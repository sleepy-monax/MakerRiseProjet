﻿using Maker.RiseEngine.Core.UserInterface.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.RiseEngine.Core.SceneManager.Scenes.Menu
{
    public class MenuNewWorld : Scene
    {
        Panel rootContainer;
        Panel controlContainer;

        Button CreateNewWorldButton;

        public override void OnLoad()
        {

            rootContainer = new Panel(new Rectangle(0, 0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight), Color.White);
            rootContainer.Padding = new UserInterface.ControlPadding(16);
            rootContainer.ControlDock = UserInterface.Dock.Fill;

            controlContainer = new Panel(new Rectangle(0, 0, 0, 96), Color.White);
            controlContainer.Padding = new UserInterface.ControlPadding(16);
            controlContainer.ControlDock = UserInterface.Dock.Down;

            CreateNewWorldButton = new Button("Créer le nouveau monde", new Rectangle(0, 0, 480, 64), Color.White);
            CreateNewWorldButton.ControlDock = UserInterface.Dock.Right;

            rootContainer.AddChild(controlContainer);
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {



        }

        public override void OnUnload()
        {



        }

        public override void OnUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {



        }

    }
}
