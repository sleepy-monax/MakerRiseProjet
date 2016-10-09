using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RiseEngine.Core.GameObject.Event;
using RiseEngine.Core.Audio;

namespace RiseEngine.Core.GameObject.Tiles
{
    public class Tile : ITile
    {
        public System.Drawing.Color MapColor { get;set; }

        public int MaxVariantCount { get; set; }

        public string Name { get; set; }

        public List<Rendering.SpriteSheets.Sprite> Variant;

        SoundEffectColection SE;
        bool AsSE = false;

        public Tile(string _Name, string[] _SpriteVariant, string _SpriteSheet, System.Drawing.Color _MapColor)
        {
            Name = _Name;
            MapColor = _MapColor;

            this.Variant = new List<Rendering.SpriteSheets.Sprite>();

            foreach (string str in _SpriteVariant)
            {

                this.Variant.Add(GameObjectsManager.SpriteSheets[_SpriteSheet].GetSprite(str));

            }


        }

        public void SetSouneffect(SoundEffectColection _SE)
        {

            SE = _SE;
            AsSE = true;

        }

        public void OnDraw(GameObjectEventArgs e, SpriteBatch spritebatch, GameTime gametime)
        {
            Variant[e.ParrentTile.Variant].Draw(spritebatch, new Microsoft.Xna.Framework.Rectangle(new Microsoft.Xna.Framework.Point(e.OnScreenLocation.X - e.World.Camera.Zoom / 2, e.OnScreenLocation.Y - e.World.Camera.Zoom / 2), new Microsoft.Xna.Framework.Point(e.World.Camera.Zoom * 2, e.World.Camera.Zoom * 2)), Microsoft.Xna.Framework.Color.White, gametime);

        }

        public void OnEntityWalkIn(GameObjectEventArgs e, GameTime gametime)
        {
            if (AsSE)
            {

                Audio.SoundEffectEngine.PlaySoundEffects(SE);

            }
        }

        public void OnTick(GameObjectEventArgs e, GameTime gametime)
        {
            
        }

        public void OnUpdate(GameObjectEventArgs e, KeyboardState keyboard, MouseState mouse, GameTime gametime)
        {
         
        }
    }
}
