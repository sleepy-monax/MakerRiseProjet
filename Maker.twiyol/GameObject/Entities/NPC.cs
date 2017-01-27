using Maker.RiseEngine.Core.Input;
using Maker.twiyol.Game.WorldDataStruct;
using Maker.twiyol.GameObject.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.twiyol.GameObject.Entities
{
    public class NPC : IEntity
    {
        public bool CanBeKilled { get; set; }

        public bool CanTakeDamage { get; set; }

        public string GameObjectName { get; set; }

        public int MaxLife { get; set; }

        public int MaxVariantCount { get; set; }

        public int MoveRunSpeed { get; set; }

        public int MoveSpeed { get; set; }

        public string PluginName { get; set; }

        public float GetDamage(GameObjectEventArgs e)
        {
            return 0;
        }

        public float GetDefence(GameObjectEventArgs e)
        {
            return 0;
        }

        public void OnDamageTaken(GameObjectEventArgs e)
        {
            
        }

        public void OnDraw(GameObjectEventArgs e, SpriteBatch spritebatch, GameTime gametime)
        {
            
        }

        public void OnEntityDestroy(GameObjectEventArgs e)
        {
            
        }

        public void OnEntityInteract(GameObjectEventArgs e, GameObjectEventArgs entityInteracts)
        {
            
        }

        public void OnEntityKilled(GameObjectEventArgs e, DataEntity entityKills)
        {
            
        }

        public void OnGameObjectAdded()
        {
            
        }

        public void OnTick(GameObjectEventArgs e, GameTime gametime)
        {
            
        }

        public void OnUpdate(GameObjectEventArgs e, GameInput playerInput, GameTime gametime)
        {
            
        }
    }
}
