using Maker.RiseEngine.Core.GameComponent;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using Maker.twiyol.Game.GameUtils;
using Maker.twiyol.Game.WorldDataStruct;
using Maker.twiyol.GameObject.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Maker.twiyol.GameObject.Entities
{
    public class Entity : IEntity
    {

        public string GameObjectName { get; set; }
        public string PluginName { get; set; }

        //Drawing Property
        public Rectangle DrawBox;
        public List<Sprite> Variant;

        //Determine la position de l'entiter sur la case
        public Vector2 SpriteLocation = new Vector2(0, -1f);

        public int MaxVariantCount { get; set; }

        public int MoveSpeed { get; set; } = 5;

        public int MaxHeal { get; set; } = 20;

        public int MoveRunSpeed { get; set; } = 10;

        public bool CanTakeDamage { get; set; } = false;

        public bool CanBeKilled { get; set; } = false;

        public DrawLayer Layer => throw new NotImplementedException();

        public Entity(string[] _SpriteVariant, string _SpriteSheet, Vector2 _SpriteLocation)
        {
            Variant = new List<Sprite>();

            foreach (string str in _SpriteVariant)
            {
                Variant.Add(GameComponentManager.GetGameObject<SpriteSheet>(_SpriteSheet.Split('.')[0], _SpriteSheet.Split('.')[1]).GetSprite(str));
            }
            SpriteLocation = _SpriteLocation;
            DrawBox = new Rectangle(Point.Zero, new Point(Variant[0].sprites[0].Width, Variant[0].sprites[0].Height));
            this.MaxVariantCount = Variant.Count - 1;
        }

        public void OnDraw(GameObjectEventArgs e, SpriteBatch spritebatch, GameTime gametime)
        {
            Variant[e.ParrentEntity.Variant].Draw(spritebatch, new Rectangle(
                   e.OnScreenLocation.X + (int)(e.Game.Camera.TileUnit * (this.SpriteLocation.X + e.ParrentEntity.OnTileOffsetX)),
                   e.OnScreenLocation.Y + (int)(e.Game.Camera.TileUnit * (this.SpriteLocation.Y + +e.ParrentEntity.OnTileOffsetY)),
                   this.DrawBox.Width * e.Game.Camera.TileUnit, this.DrawBox.Height * e.Game.Camera.TileUnit), Color.White, gametime);
        }

        public virtual void OnTick(GameObjectEventArgs e, GameTime gametime)
        {

        }

        public virtual void OnUpdate(GameObjectEventArgs e, GameInput playerInput, GameTime gametime)
        {

        }

        public void OnEntityInteract(GameObjectEventArgs eThisEntity, GameObjectEventArgs eInteratingEntity)
        {

        }

        public void OnGameObjectAdded()
        {

        }

        public float damage = 5;
        public float GetDamage(GameObjectEventArgs e)
        {
            return damage;
        }

        public float defence = 0;
        public float GetDefence(GameObjectEventArgs e)
        {
            return defence;
        }

        public void OnDamagesTaken(GameObjectEventArgs e)
        {

        }

        public void OnEntityDestroy(GameObjectEventArgs e)
        {

        }

        public void OnEntityKilled(GameObjectEventArgs e, DataEntity entityKills)
        {
            e.Game.World.RemoveEntityData(e.CurrentLocation);
        }
    }
}
