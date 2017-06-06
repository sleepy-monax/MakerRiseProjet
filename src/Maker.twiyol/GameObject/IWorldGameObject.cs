using Maker.RiseEngine;
using Maker.RiseEngine.GameObjects;
using Maker.RiseEngine.Input;
using Maker.Twiyol.GameObject.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Maker.Twiyol.GameObject
{
    public enum DrawLayer
    {
        A,
        B,
        C,
        D,
        E,
    }
    public interface IWorldGameObject : IGameObject
    {

        int MaxVariantCount { get; }

        void Tick(GameObjectEventArgs e, GameTime gametime);
        void Update(GameObjectEventArgs e, GameInput playerInput, GameTime gametime);
        void Draw(GameObjectEventArgs e, SpriteBatch spritebatch, GameTime gametime);

    }
}
