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
    public interface IEntity : IWorldGameObject
    {

        int MaxLife { get; set; }
        int MoveSpeed { get; set; }
        int MoveRunSpeed { get; set; }

        float GetDamage(Event.GameObjectEventArgs e);
        float GetDefence(Event.GameObjectEventArgs e);
        void OnDamageTaken(Event.GameObjectEventArgs e);
        void OnEntityDestroy(Event.GameObjectEventArgs e);


        void OnEntityInteract(Event.GameObjectEventArgs e, Event.GameObjectEventArgs eInteratingEntity);


    }
}
