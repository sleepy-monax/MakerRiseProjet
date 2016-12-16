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
                onDraw(spriteBatch, gameTime);
            }
        }
        public void sceneUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {
            if (!Pause)
            {
                onUpdate(mouse, keyBoard, gameTime);
            }
        }

        // Event.
        public abstract void onDraw(SpriteBatch spriteBatch, GameTime gameTime);
        public abstract void onUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime);

        public abstract void OnLoad();
    }
}
