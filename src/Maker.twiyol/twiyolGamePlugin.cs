using Maker.RiseEngine.Core.Plugin;
using Maker.RiseEngine.Core.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Input;
using System;
using Maker.Twiyol.Scenes.Menu;

namespace Maker.Twiyol
{
    class TwiyolGamePlugin : IPlugin
    {
        public string PluginName => "TWIYOL";

        public void Initialize(PluginLoader pluginLoader, GameEngine engine)
        {
            MainMenuBackground mainMenuBackground = new MainMenuBackground();
            MainMenu mainMenu = new MainMenu();

            engine.sceneManager.AddScene(mainMenuBackground);
            engine.sceneManager.AddScene(mainMenu);

            mainMenuBackground.Show();
            mainMenu.Show();
        }
    }
}
