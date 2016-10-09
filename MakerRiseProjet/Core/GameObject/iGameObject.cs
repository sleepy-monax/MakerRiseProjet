using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.GameObject
{
    public interface IGameObject
    {
        string Name { get; set; }
        int MaxVariantCount { get; set; }

        void OnTick(Event.GameObjectEventArgs e, GameTime gametime);
        void OnUpdate(Event.GameObjectEventArgs e, KeyboardState keyboard, MouseState mouse, GameTime gametime);
        void OnDraw(Event.GameObjectEventArgs e, SpriteBatch spritebatch, GameTime gametime);

    }
}
