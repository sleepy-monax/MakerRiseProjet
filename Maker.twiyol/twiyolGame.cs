using Maker.RiseEngine.Core.Plugin;
using Maker.RiseEngine.Core.GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Input;
using System;

namespace Maker.twiyol
{
    class twiyolGame : IRiseGame
    {
        public string Name { get; } = "twiyol";

        public void OnEngineInitialization(RiseEngine.Core.RiseEngine Game)
        {

        }

        public void OnLoadContent()
        {

        }

        public void OnDraw(SpriteBatch spritebatch, GameTime gametime)
        {

        }

        public void OnUpdate(PlayerInput playerInput)
        {

        }

        public void Initialize<PluginType>(PluginLoader<PluginType> pluginLoader) where PluginType : IPlugin
        {
            
        }
    }
}
