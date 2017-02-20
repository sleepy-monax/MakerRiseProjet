using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Physic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace Maker.RiseEngine.Core.Scenes
{
    public abstract class Scene : IDisposable
    {

        public bool Pause { get; set; } = true;
        public bool Visible { get; set; } = false;
        public RiseEngine RiseEngine;

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
        public void sceneUpdate(GameInput playerInput, GameTime gameTime)
        {
            if (!Pause)
            {
                OnUpdate(playerInput, gameTime);
            }
        }

        // Event.
        public abstract void OnDraw(SpriteBatch spriteBatch, GameTime gameTime);
        public abstract void OnUpdate(GameInput playerInput, GameTime gameTime);

        public abstract void OnLoad();
        public abstract void OnUnload();

        public void Dispose()
        {
            RiseEngine = null;
            GC.SuppressFinalize(this);
        }
    }
}
