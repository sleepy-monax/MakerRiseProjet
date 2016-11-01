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
    public interface ITile : IWorldGameObject
    {

        System.Drawing.Color MapColor { get; set; }

        void OnEntityWalkIn(Event.GameObjectEventArgs e, GameTime gametime);

    }
}
