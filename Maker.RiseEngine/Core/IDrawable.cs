using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core
{
    public interface IDrawable
    {

        void update(MouseState mouse, KeyboardState keyBoard,GameTime gameTime);
        void draw(GameTime gameTime);

    }
}
