using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.GameComponent;
using Maker.RiseEngine.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Maker.twiyol.GameObject
{
    public enum DrawLayer
    {
        A,
        B,
        C,
        D,
        E,
    }
    public interface IWorldGameObject : RiseEngine.Core.GameComponent.IGameObject
    {

        int MaxVariantCount { get; }
        DrawLayer Layer { get; }

        void OnTick(Event.GameObjectEventArgs e, GameTime gametime);
        void OnUpdate(Event.GameObjectEventArgs e, GameInput playerInput, GameTime gametime);
        void OnDraw(Event.GameObjectEventArgs e, SpriteBatch spritebatch, GameTime gametime);

    }
}
