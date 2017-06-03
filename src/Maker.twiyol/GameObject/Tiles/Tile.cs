using Maker.RiseEngine.Core.Audio;
using Maker.RiseEngine.Core.GameObjects;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using Maker.Twiyol.GameObject.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Maker.Twiyol.GameObject.Tiles
{
    public class Tile : ITile
    {
        public string GameObjectName { get; set; }
        public string PluginName { get; set; }
        public Color MapColor { get; set; }
        public int MaxVariantCount { get; set; }
        public List<Sprite> SpriteVariante;

        SoundEffectColection walkInSoundEffect;

        public Tile(string[] tileVariantSprite, int spriteSheetID, Color onMiniMapColor)
        {
            SpriteVariante = new List<Sprite>();

            MapColor = onMiniMapColor;
            MaxVariantCount = SpriteVariante.Count;

            foreach (string str in tileVariantSprite)
            {
                SpriteVariante.Add(GameObjectManager.GetGameObject<SpriteSheet>(spriteSheetID).GetSprite(str));
            }
        }

        public void OnGameObjectAdded()
        {

        }

        public virtual void Draw(GameObjectEventArgs e, SpriteBatch spritebatch, GameTime gametime)
        {
            SpriteVariante[e.ParrentTile.Variant].Draw(spritebatch,
                new Rectangle(
                new Point(e.OnScreenLocation.X - e.Game.Camera.TileUnit / 2, e.OnScreenLocation.Y - e.Game.Camera.TileUnit / 2),
                new Point(e.Game.Camera.TileUnit * 2, e.Game.Camera.TileUnit * 2)),
                Color.White, gametime);

        }

        public void Update(GameObjectEventArgs e, GameInput playerInput, GameTime gametime)
        {

        }

        public void Tick(GameObjectEventArgs e, GameTime gametime)
        {

        }

        public void OnEntityWalkIn(GameObjectEventArgs e, GameTime gametime)
        {
            /*
            if (walkInSoundEffect != null)
                SoundEffectManager.PlaySoundEffect(walkInSoundEffect);
                */
        }

        public void SetWalkInSoundEffect(SoundEffectColection SoundEffect)
        {

            walkInSoundEffect = SoundEffect;

        }
    }
}
