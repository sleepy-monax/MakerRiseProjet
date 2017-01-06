﻿using Maker.RiseEngine.Core.Audio;
using Maker.RiseEngine.Core.GameObject;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using Maker.twiyol.GameObject.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Maker.twiyol.GameObject.Tiles
{
    public class Tile : ITile
    {
        public string gameObjectName { get; set; }
        public string pluginName { get; set; }


        public System.Drawing.Color MapColor { get; set; }

        public int MaxVariantCount { get; set; }



        public List<Sprite> Variant;

        SoundEffectColection SE;

        public Tile(string[] _SpriteVariant, string _SpriteSheet, System.Drawing.Color _MapColor)
        {
            MapColor = _MapColor;

            this.Variant = new List<Sprite>();

            foreach (string str in _SpriteVariant)
            {

                Variant.Add(GameObjectManager.GetGameObject<SpriteSheet>(_SpriteSheet.Split('.')[0], _SpriteSheet.Split('.')[1]).GetSprite(str));

            }
            MaxVariantCount = Variant.Count;

        }

        public void SetSoundEffect(SoundEffectColection _SE)
        {

            SE = _SE;

        }

        public void OnDraw(GameObjectEventArgs e, SpriteBatch spritebatch, GameTime gametime)
        {
            Variant[e.ParrentTile.Variant].Draw(spritebatch,
                new Rectangle(
                new Point(e.OnScreenLocation.X - e.World.Camera.Zoom / 2, e.OnScreenLocation.Y - e.World.Camera.Zoom / 2),
                new Point(e.World.Camera.Zoom * 2, e.World.Camera.Zoom * 2)),
                Color.White, gametime);

        }

        public void OnEntityWalkIn(GameObjectEventArgs e, GameTime gametime)
        {

            if (SE != null)
                SoundEffectEngine.PlaySoundEffect(SE);

        }

        public void OnTick(GameObjectEventArgs e, GameTime gametime)
        {

        }

        public void OnUpdate(GameObjectEventArgs e, PlayerInput playerInput, GameTime gametime)
        {

        }

        public void OnGameObjectAdded()
        {

        }
    }
}
