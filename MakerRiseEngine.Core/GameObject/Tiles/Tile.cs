using Maker.RiseEngine.Core.Audio;
using Maker.RiseEngine.Core.GameObject.Event;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Maker.RiseEngine.Core.GameObject.Tiles
{
    public class Tile : ITile
    {
        public string gameObjectName { get; set; }
        public string pluginName { get; set; }


        public System.Drawing.Color MapColor { get;set; }

        public int MaxVariantCount { get; set; }

        

        public List<Sprite> Variant;

        SoundEffectColection SE;
        bool AsSoundEffect = false;

        public Tile(string[] _SpriteVariant, string _SpriteSheet, System.Drawing.Color _MapColor)
        {
            MapColor = _MapColor;

            this.Variant = new List<Sprite>();

            foreach (string str in _SpriteVariant)
            {

                Variant.Add(GameObjectsManager.GetGameObject<SpriteSheet>(_SpriteSheet.Split('.')[0], _SpriteSheet.Split('.')[1]).GetSprite(str));

            }
            MaxVariantCount = Variant.Count;

        }

        public void SetSoundEffect(SoundEffectColection _SE)
        {

            SE = _SE;
            AsSoundEffect = true;

        }

        public void OnDraw(GameObjectEventArgs e, SpriteBatch spritebatch, GameTime gametime)
        {
            Variant[e.ParrentTile.Variant].Draw(spritebatch, 
                new Microsoft.Xna.Framework.Rectangle(

                new Microsoft.Xna.Framework.Point(e.OnScreenLocation.X - e.World.Camera.Zoom / 2, e.OnScreenLocation.Y - e.World.Camera.Zoom / 2), 
                new Microsoft.Xna.Framework.Point(e.World.Camera.Zoom * 2, e.World.Camera.Zoom * 2)
                
                ),
                
                Microsoft.Xna.Framework.Color.White, gametime);

        }

        public void OnEntityWalkIn(GameObjectEventArgs e, GameTime gametime)
        {
            if (AsSoundEffect)
            {

                SoundEffectEngine.PlaySoundEffect(SE);

            }
        }

        public void OnTick(GameObjectEventArgs e, GameTime gametime)
        {
            
        }

        public void OnUpdate(GameObjectEventArgs e, KeyboardState keyboard, MouseState mouse, GameTime gametime)
        {
         
        }


        public void OnGameObjectAdded()
        {

        }
    }
}
