using Microsoft.Xna.Framework;
using Maker.RiseEngine.Core.Audio;
using Maker.RiseEngine.Core.MathExt;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;
using Maker.RiseEngine.Core.Content;
using Maker.RiseEngine.Core.Plugin;
using System;
using Maker.twiyol.AI.Action;
using Maker.twiyol.GameObject.Items;
using Maker.twiyol.GameObject;
using Maker.twiyol.GameObject.Tiles;
using Maker.RiseEngine.Core.GameObject;
using Maker.twiyol.Game.WorldDataStruct;
using Maker.twiyol.Game;
using Maker.twiyol.Game.GameUtils;
using Maker.twiyol.GameObject.Entities;
using Maker.twiyol.AI.Entites;
using Maker.twiyol.Events;
using Maker.twiyol.Generator.EntitiesDistribution;

namespace Maker.twiyal.Base
{
    class Plugin : IPlugin
    {
        public string Name
        {
            get
            {
                return "TWIYOL_Base";
            }
        }

        public void Initialize<PluginType>(PluginLoader<PluginType> pluginLoader) where PluginType : IPlugin
        {
            pluginLoader.Include(this, "TWIYOL");

            this.AddGameObject("Move", new Move());
            this.AddGameObject("Attack", new Attack());

            //TileSheet
            this.AddGameObject("Tilesheet_Terrain", new SpriteSheet(Name, ContentEngine.Texture2D(Name, "Tilesheet_Terrain"), "Tilesheet_Terrain", new Point(32)));
            this.AddGameObject("Tilesheet_Entity", new SpriteSheet(Name, ContentEngine.Texture2D(Name, "Tilesheet_Entity"), "Tilesheet_Entity", new Point(16)));
            this.AddGameObject("Tilesheet_Item", new SpriteSheet(Name, ContentEngine.Texture2D(Name, "Tilesheet_Item"), "Tilesheet_Item", new Point(16)));
            this.AddGameObject("Tilesheet_Creatures", new SpriteSheet(Name, ContentEngine.Texture2D(Name, "Tilesheet_Creatures"), "Tilesheet_Creatures", new Point(16)));
            this.AddGameObject("Tilesheet_Water", new SpriteSheet(Name, ContentEngine.Texture2D(Name, "Tilesheet_Water"), "Tilesheet_Water", new Point(32)));

            //Items
            this.AddGameObject("Stick", new Item(ItemType.Crafting, new string[] { "Stick" }, $"{Name}.Tilesheet_Item"));
            this.AddGameObject("Apple", new Item(ItemType.Food, new string[] { "AppleRed", "AppleGreen", "AppleYellow" }, $"{Name}.Tilesheet_Item"));

            //Tiles
            Tile Grass = new Tile(new string[] { "Grass0", "Grass1", "Grass2", "Grass3" }, $"{Name}.Tilesheet_Terrain", System.Drawing.Color.FromArgb(36, 81, 11));
            Grass.SetSoundEffect(new SoundEffectColection(Name, "dirtfootstep"));
            this.AddGameObject("Grass", Grass);
            this.AddGameObject("FlowerOnGrass", new Tile(new string[] { "YellowFlowerGrass", "PurpleFlowerGrass" }, $"{Name}.Tilesheet_Terrain", System.Drawing.Color.FromArgb(36, 81, 11)) { });
            this.AddGameObject("Sand", new Tile(new string[] { "Sand0", "Sand1", "Sand2", "Sand3" }, $"{Name}.Tilesheet_Terrain", System.Drawing.Color.Yellow));
            this.AddGameObject("Stone", new Tile(new string[] { "Stone0", "Stone1", "Stone2", "Stone3" }, $"{Name}.Tilesheet_Terrain", System.Drawing.Color.Gray));
            this.AddGameObject("Dirt", new Tile(new string[] { "Dirt0", "Dirt1", "Dirt2", "Dirt3" }, $"{Name}.Tilesheet_Terrain", System.Drawing.Color.Gray));
            this.AddGameObject("Water", new Tile(new string[] { "Water" }, $"{Name}.Tilesheet_Water", System.Drawing.Color.Blue));

            //Entity
            this.AddGameObject("BigTree", new Entity(new string[] { "BigTree0" }, $"{Name}.Tilesheet_Entity", new Vector2(-0.5f, -3f)));
            this.AddGameObject("PinTree", new Entity(new string[] { "PinTree0" }, $"{Name}.Tilesheet_Entity", new Vector2(-0.5f, -3f)));
            this.AddGameObject("LargeTree", new Entity(new string[] { "LargeTree0" }, $"{Name}.Tilesheet_Entity", new Vector2(-0.5f, -1f)));
            this.AddGameObject("TreeStump", new Entity(new string[] { "stump0", "stump1", "stump2" }, $"{Name}.Tilesheet_Entity", new Vector2(0)));
            this.AddGameObject("Rock", new Entity(new string[] { "Rock0", "Rock1", "Rock2", "Rock3" }, $"{Name}.Tilesheet_Entity", new Vector2(0)));
            this.AddGameObject("Plant", new Entity(new string[] { "Plant0", "Plant1", "Plant2", "Plant3", "Plant4", "Plant5" }, $"{Name}.Tilesheet_Entity", new Vector2(0)));
            this.AddGameObject("Player", new Creature(new PlayerAI(0, 1, 2, 3, 4, 5, 6, 7), new string[] { "Player_Move_Up", "Player_Move_Down", "Player_Move_Left", "Player_Move_Right", "Player_Idle_Up", "Player_Idle_Down", "Player_Idle_Left", "Player_Idle_Right" }, $"{Name}.Tilesheet_Creatures", new Vector2(0)));

            this.AddGameObject("Cactus", new Entity(new string[] { "Cactus1", "Cactus2" }, $"{Name}.Tilesheet_Entity", new Vector2(0)));
            this.AddGameObject("TaleCactus", new Entity(new string[] { "Cactus0" }, $"{Name}.Tilesheet_Entity", new Vector2(0, -1f)));


            //Biome
            this.AddGameObject("Plains", new Biome(new PerlinDistribution(0.1f, 0),
                 new KeyWeightPair<int>[] {
                    new KeyWeightPair<int>(this.GetGameObjectIndex("Rock"), 0.5),
                    new KeyWeightPair<int>(this.GetGameObjectIndex("Plant"), 0.5)
                 },
                 new KeyWeightPair<int>[] {

                    new KeyWeightPair<int>(this.GetGameObjectIndex("Grass"), 0.9),
                    new KeyWeightPair<int>(this.GetGameObjectIndex("FlowerOnGrass"), 0.1)
                 }));



            this.AddGameObject("Forest", new Biome(new PerlinDistribution(0.1f, 0),
                new KeyWeightPair<int>[] {
                    new KeyWeightPair<int>(this.GetGameObjectIndex("BigTree"), 0.3),
                    new KeyWeightPair<int>(this.GetGameObjectIndex("PinTree"), 0.3),
                    new KeyWeightPair<int>(this.GetGameObjectIndex("LargeTree"), 0.2),
                    new KeyWeightPair<int>(this.GetGameObjectIndex("Rock"), 0.05),
                    new KeyWeightPair<int>(this.GetGameObjectIndex("Plant"), 0.05),
                    new KeyWeightPair<int>(this.GetGameObjectIndex("TreeStump"), 0.1)
                },
                new KeyWeightPair<int>[] {
                    new KeyWeightPair<int>(this.GetGameObjectIndex("Grass"), 1)

                }));

            this.AddGameObject("Desert", new Biome(new FlatDistribution(0.1f), new KeyWeightPair<int>[] { new KeyWeightPair<int>(this.GetGameObjectIndex("Cactus"), 0.75f), new KeyWeightPair<int>(this.GetGameObjectIndex("TaleCactus"), 0.25f) }, new KeyWeightPair<int>[] { new KeyWeightPair<int>(this.GetGameObjectIndex("Sand"), 1) }));
            this.AddGameObject("River", new Biome(new FlatDistribution(0), new KeyWeightPair<int>[] { }, new KeyWeightPair<int>[] { new KeyWeightPair<int>(this.GetGameObjectIndex("Water"), 1) }));
            this.AddGameObject("Ocean", new Biome(new FlatDistribution(0), new KeyWeightPair<int>[] { }, new KeyWeightPair<int>[] { new KeyWeightPair<int>(this.GetGameObjectIndex("Water"), 1) }));


            // Add event handle.
            GameEventHandler.OnWorldGeneratingEnd += GameEventHandler_OnWorldGenerating;
        }

        private void GameEventHandler_OnWorldGenerating(object sender, EventArgs e)
        {
            WorldEventArgs w = (WorldEventArgs)e;

            DataEntity E = new DataEntity(this.GetGameObjectIndex("Player"), 0);
            E.IsFocus = true;

            w.World.RemoveEntityData(new WorldLocation(new Point(5, 5), new Point(5, 5)));
            w.World.AddEntityData(E, new WorldLocation(new Point(5, 5), new Point(5, 5)));
            w.World.Camera.FocusLocation = new WorldLocation(new Point(5, 5), new Point(5, 5));
        }
    }
}
