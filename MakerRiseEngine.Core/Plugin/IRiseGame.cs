using Maker.RiseEngine.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.RiseEngine.Core.Plugin
{
    public interface IRiseGame : IPlugin
    {

        void OnEngineInitialization(RiseEngine Game);
        void OnLoadContent();
        void OnDraw(SpriteBatch spritebatch, GameTime gametime);
        void OnUpdate(PlayerInput playerInput);

    }
}
