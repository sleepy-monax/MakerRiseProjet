using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.RiseEngine.Core.SceneManager
{
    public abstract class Scene
    {

        public bool Pause { get; set; } = true;
        public bool Visible { get; set; } = false;
        public RiseGame Game;

        public void hide()
        {
            Visible = false;
            Pause = true;
        }

        public void show()
        {
            Visible = true;
            Pause = false;
        }

        // Update and draw.
        public void sceneDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Visible)
            {
                OnDraw(spriteBatch, gameTime);
            }
        }
        public void sceneUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {
            if (!Pause)
            {
                OnUpdate(mouse, keyBoard, gameTime);
            }
        }

        // Event.
        public abstract void OnDraw(SpriteBatch spriteBatch, GameTime gameTime);
        public abstract void OnUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime);

        public abstract void OnLoad();
        public abstract void OnUnload();
    }
}
