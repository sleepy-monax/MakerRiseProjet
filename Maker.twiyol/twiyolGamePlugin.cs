using Maker.RiseEngine.Core.Plugin;
using Maker.RiseEngine.Core.GameComponent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Input;
using System;

namespace Maker.twiyol
{
    class twiyolGamePlugin : IPlugin
    {
        public string Name { get; } = "TWIYOL";

        public void Initialize<PluginType>(PluginLoader<PluginType> pluginLoader) where PluginType : IPlugin
        {
            // Stating the game...
            Scenes.Menu.MenuBackground b = new Scenes.Menu.MenuBackground();
            Scenes.Menu.MenuMain m = new Scenes.Menu.MenuMain();
            Engine.RiseEngine.sceneManager.AddScene(b);
            Engine.RiseEngine.sceneManager.AddScene(m);
            b.show();
            m.show();
        }
    }
}
