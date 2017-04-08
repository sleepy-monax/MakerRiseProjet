namespace Maker.twiyol.GameObject
{
    public interface IEntity : IWorldGameObject
    {

        int MaxHeal { get;}
        int MoveSpeed { get; }
        int MoveRunSpeed { get;}

        float GetDamages(Event.GameObjectEventArgs e);
        float GetDefence(Event.GameObjectEventArgs e);

        void OnDamagesTaken(Event.GameObjectEventArgs e);
        void OnEntityInteract(Event.GameObjectEventArgs e, Event.GameObjectEventArgs entityInteracts);
        void OnEntityKilled(Event.GameObjectEventArgs e, Game.WorldDataStruct.DataEntity entityKills);

    }
}
