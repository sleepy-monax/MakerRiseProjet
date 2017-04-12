using Maker.RiseEngine.Core.GameObjects;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;

using Maker.twiyol.Game.GameUtils;
using Maker.twiyol.Game.WorldDataStruct;
using Maker.twiyol.GameObject.Event;
using Maker.twiyol.Inventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

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

        public int MoveSpeed => 5;

        public int MaxHeal => 20;

        public int MoveRunSpeed => 10;

        public DrawLayer Layer => throw new NotImplementedException();

        public List<ItemDrop> DropList;

        public Entity(string[] _SpriteVariant, int spriteSheetID, Vector2 _SpriteLocation)
        {
            Variant = new List<Sprite>();

            foreach (string str in _SpriteVariant)
            {
                Variant.Add(GameComponentManager.GetGameObject<SpriteSheet>(spriteSheetID).GetSprite(str));
            }
            SpriteLocation = _SpriteLocation;
            DrawBox = new Rectangle(Point.Zero, new Point(Variant[0].sprites[0].Width, Variant[0].sprites[0].Height));
            this.MaxVariantCount = Variant.Count - 1;
        }

        public virtual void Draw(GameObjectEventArgs e, SpriteBatch spritebatch, GameTime gametime)
        {
            Variant[e.ParrentEntity.Variant].Draw(spritebatch, new Rectangle(
                   e.OnScreenLocation.X + (int)(e.Game.Camera.TileUnit * (this.SpriteLocation.X + e.ParrentEntity.OnTileOffsetX)),
                   e.OnScreenLocation.Y + (int)(e.Game.Camera.TileUnit * (this.SpriteLocation.Y + +e.ParrentEntity.OnTileOffsetY)),
                   this.DrawBox.Width * e.Game.Camera.TileUnit, this.DrawBox.Height * e.Game.Camera.TileUnit), Color.White, gametime);
        }

        public virtual void Tick(GameObjectEventArgs e, GameTime gametime)
        {

        }

        public virtual void Update(GameObjectEventArgs e, GameInput playerInput, GameTime gametime)
        {

        }

        public void OnEntityInteract(GameObjectEventArgs eThisEntity, GameObjectEventArgs eInteratingEntity)
        {

        }

        public void OnGameObjectAdded()
        {

        }

        public float damage = 5;
        public float GetDamages(GameObjectEventArgs e)
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

        public void OnEntityKilled(GameObjectEventArgs e, DataEntity entityKills)
        {
            if (entityKills.Tags.HasTag("inventory"))
            {
                // give somme loot to the killer.
                DataInventory KillerInventory =  entityKills.Tags.GetTag<DataInventory>("inventory", null);
                Random rnd = new Random();
                foreach (var item in DropList)
                {
                    if (rnd.NextDouble() >= item.DropChance)
                    {
                        KillerInventory.AddItem(new DataItem(item.ID, item.Variant, item.Count));
                    }
                }

            }

            e.Game.World.RemoveEntityData(e.CurrentLocation);
        }
    }
}
