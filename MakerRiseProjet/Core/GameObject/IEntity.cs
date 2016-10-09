using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RiseEngine.Core.World.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.GameObject
{
    public interface IEntity : IGameObject
    {

        int MaxLife { get; set; }
        int MoveSpeed { get; set; }

    }
}
