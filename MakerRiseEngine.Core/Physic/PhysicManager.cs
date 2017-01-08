using Maker.RiseEngine.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Maker.RiseEngine.Core.Physic
{
    public class PhysicManager : IDrawable
    {
        public List<PhysicObject> Object;

        public PhysicManager() {
            Object = new List<PhysicObject>(); 
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }

        public void Update(PlayerInput playerInput, GameTime gameTime)
        {
            
        }
    }
}
