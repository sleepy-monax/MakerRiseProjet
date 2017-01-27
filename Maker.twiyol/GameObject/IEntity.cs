namespace Maker.twiyol.GameObject
{
    public interface IEntity : IWorldGameObject
    {

        int MaxLife { get; set; }
        int MoveSpeed { get; set; }
        int MoveRunSpeed { get; set; }

        bool CanTakeDamage { get; set; }
        bool CanBeKilled { get; set; }

        float GetDamage(Event.GameObjectEventArgs e);
        float GetDefence(Event.GameObjectEventArgs e);

        /// <summary>
        /// This event is raise when the entity take damages.
        /// </summary>
        /// <param name="e">GameObjectEventArgs.</param>
        void OnDamageTaken(Event.GameObjectEventArgs e);
        
        /// <summary>
        /// This event is raise when the entity is destroy.
        /// </summary>
        /// <param name="e"></param>
        void OnEntityDestroy(Event.GameObjectEventArgs e);
        
        /// <summary>
        /// This event is raise when a other entity is enterating
        /// </summary>
        /// <param name="e"></param>
        /// <param name="entityInteracts"></param>
        void OnEntityInteract(Event.GameObjectEventArgs e, Event.GameObjectEventArgs entityInteracts);
        void OnEntityKilled(Event.GameObjectEventArgs e, Game.WorldDataStruct.DataEntity entityKills);

    }
}
