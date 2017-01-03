
using Maker.twiyol.GameObject.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Maker.twiyol.GameObject.Entities
{
    public class Creature : Entity
    {
        public AI.AIbase IA { get; set; }

        public Creature(AI.AIbase _IA, string[] _SpriteVariant, string _SpriteSheet, Vector2 _SpriteLocation) : base(_SpriteVariant, _SpriteSheet, _SpriteLocation)
        {
            IA = _IA;
        }

        public override void OnTick(GameObjectEventArgs e, GameTime gametime) { }

        public override void OnUpdate(GameObjectEventArgs e, KeyboardState keyboard, MouseState mouse, GameTime gametime)
        {
            IA.Tick(e, keyboard, mouse, gametime);
            IA.ExecuteAction(e, gametime);
        }

    }
}
