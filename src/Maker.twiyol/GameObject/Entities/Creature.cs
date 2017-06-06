using Maker.RiseEngine.Input;

using Maker.Twiyol.AI;
using Maker.Twiyol.GameObject.Event;

using Microsoft.Xna.Framework;

namespace Maker.Twiyol.GameObject.Entities
{
    public class Creature : Entity
    {
        public AIbase CreatureAI { get; set; }

        public Creature(AIbase creatureAI, string[] _SpriteVariant, int spriteSheetID, Vector2 _SpriteLocation) : base(_SpriteVariant, spriteSheetID, _SpriteLocation)
        {
            CreatureAI = creatureAI;
        }

        public override void Tick(GameObjectEventArgs e, GameTime gametime) { }

        public override void Update(GameObjectEventArgs e, GameInput playerInput, GameTime gametime)
        {
            CreatureAI.Tick(e, playerInput, gametime);
            CreatureAI.ExecuteAction(e, gametime);
        }

    }
}
