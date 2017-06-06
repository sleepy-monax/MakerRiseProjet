using Maker.RiseEngine.Plugin;
using Maker.RiseEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maker.RiseEngine;
using Maker.RiseEngine.Input;
using System;
using Maker.Twiyol.Scenes.Menu;

namespace Maker.Twiyol
{
    class TwiyolGamePlugin : IPlugin
    {
        public string Name => "TWIYOL";

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
