using Maker.RiseEngine.Core.Plugin;
using Maker.RiseEngine.Core.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Input;
using System;

namespace Maker.Twiyol
{
    class twiyolGamePlugin : IPlugin
    {
        public string PluginName { get; } = "TWIYOL";

        public void Initialize<PluginType>(PluginLoader<PluginType> pluginLoader, engine ENGINE) where PluginType : IPlugin
        {
            // Stating the game...
            Scenes.Menu.MenuBackground b = new Scenes.Menu.MenuBackground();
            Scenes.Menu.MenuMain m = new Scenes.Menu.MenuMain();
            rise.ENGINE.ScenesManager.AddScene(b);
            rise.ENGINE.ScenesManager.AddScene(m);
            b.show();
            m.show();
        }
    }
}
