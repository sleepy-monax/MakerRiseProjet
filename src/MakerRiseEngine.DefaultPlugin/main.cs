using Maker.RiseEngine.Core.Audio;
using Maker.RiseEngine.Core.Ressources;
using Maker.RiseEngine.Core.GameObjects;
using Maker.RiseEngine.Core.MathExt;
using Maker.RiseEngine.Core.Plugin;
using Maker.RiseEngine.Core.Rendering.SpriteSheets;

using Maker.Twiyol.AI.Action;
using Maker.Twiyol.AI.Entites;
using Maker.Twiyol.Events;
using Maker.Twiyol.Game.GameUtils;
using Maker.Twiyol.Game.WorldDataStruct;
using Maker.Twiyol.GameObject;
using Maker.Twiyol.GameObject.Entities;
using Maker.Twiyol.GameObject.Items;
using Maker.Twiyol.GameObject.Tiles;
using Maker.Twiyol.Generator.EntitiesDistribution;

using Microsoft.Xna.Framework;
using System;
using Maker.RiseEngine.Core;

namespace Maker.twiyol_Base
{
    public class Plugin : IPlugin
    {
        public string PluginName
        {
            get
            {
                return "TWIYOL_Base";
            }
        }

        

        public void Initialize<PluginType>(PluginLoader<PluginType> pluginLoader, engine ENGINE) where PluginType : IPlugin
        {
            pluginLoader.Include(this, "TWIYOL");

            // AI actions.
            // ----------------------------------------------------------------

            this.AddGameObject("Move", new Move());
            this.AddGameObject("Attack", new Attack());

            // TileSheets
            // ----------------------------------------------------------------

            var TILESHEET_TERRAIN   = this.AddGameObject("Tilesheet_Terrain", new SpriteSheet(PluginName, ENGINE.RESSOUCES.GetTexture2D(PluginName, "Tilesheet_Terrain"), "Tilesheet_Terrain", new Point(32)));
            var TILESHEET_ENTITY    = this.AddGameObject("Tilesheet_Entity", new SpriteSheet(PluginName, ENGINE.RESSOUCES.GetTexture2D(PluginName, "Tilesheet_Entity"), "Tilesheet_Entity", new Point(16)));
            var TILESHEET_ITEM      = this.AddGameObject("Tilesheet_Item", new SpriteSheet(PluginName, ENGINE.RESSOUCES.GetTexture2D(PluginName, "Tilesheet_Item"), "Tilesheet_Item", new Point(16)));
            var TILESHEET_CREATURES = this.AddGameObject("Tilesheet_Creatures", new SpriteSheet(PluginName, ENGINE.RESSOUCES.GetTexture2D(PluginName, "Tilesheet_Creatures"), "Tilesheet_Creatures", new Point(16)));
            var TILESHEET_WATER     = this.AddGameObject("Tilesheet_Water", new SpriteSheet(PluginName, ENGINE.RESSOUCES.GetTexture2D(PluginName, "Tilesheet_Water"), "Tilesheet_Water", new Point(32)));

            // Items
            // ----------------------------------------------------------------

            // Apples
            var ITEM_APPLE_RED    = this.AddGameObject("item_apple_red", new Item(ItemType.Food, "AppleRed", TILESHEET_ITEM));
            var ITEM_APPLE_GREEN  = this.AddGameObject("item_apple_green", new Item(ItemType.Food, "AppleGreen", TILESHEET_ITEM));
            var ITEM_APPLE_YELLOW = this.AddGameObject("item_apple_yellow", new Item(ItemType.Food, "AppleYellow", TILESHEET_ITEM));

            // Iron
            var ITEM_IRON_ORE = this.AddGameObject("item_iron_ore", new Item(ItemType.Crafting, "IronOre", TILESHEET_ITEM));

            // Misc
            var ITEM_STICK = this.AddGameObject("item_stick", new Item(ItemType.Crafting, "Stick", TILESHEET_ITEM));

            // Tiles
            // ----------------------------------------------------------------
            Tile Grass = new Tile(new string[] { "Grass0", "Grass1", "Grass2", "Grass3" }, TILESHEET_TERRAIN, System.Drawing.Color.FromArgb(36, 81, 11));
            Grass.SetSoundEffect(new SoundEffectColection(PluginName, "dirtfootstep"));

            var TILE_GRASS         = this.AddGameObject("tile_grass", Grass);
            var TILE_FLOWER = this.AddGameObject("tile_flower_on_grass", new Tile(new string[] { "YellowFlowerGrass", "PurpleFlowerGrass" }, TILESHEET_TERRAIN, System.Drawing.Color.FromArgb(36, 81, 11)) { });
            var TILE_SAND = this.AddGameObject("tile_sand", new Tile(new string[] { "Sand0", "Sand1", "Sand2", "Sand3" }, TILESHEET_TERRAIN, System.Drawing.Color.Yellow));
            var TILE_STONE         = this.AddGameObject("tile_stone", new Tile(new string[] { "Stone0", "Stone1", "Stone2", "Stone3" }, TILESHEET_TERRAIN, System.Drawing.Color.Gray));
            var TILE_DIRT          = this.AddGameObject("tile_dirt", new Tile(new string[] { "Dirt0", "Dirt1", "Dirt2", "Dirt3" }, TILESHEET_TERRAIN, System.Drawing.Color.Gray));
            var TILE_WATER         = this.AddGameObject("tile_water", new Tile(new string[] { "Water" }, TILESHEET_WATER, System.Drawing.Color.Blue));

            // Entities
            // ----------------------------------------------------------------
            var ENTITY_TREE_BIG    = this.AddGameObject("entity_tree_big", new Entity(new string[] { "BigTree0" }, TILESHEET_ENTITY, new Vector2(-0.5f, -3f)));
            var ENTITY_TREE_PIN    = this.AddGameObject("entity_tree_pin", new Entity(new string[] { "PinTree0" }, TILESHEET_ENTITY, new Vector2(-0.5f, -3f)));
            var ENTITY_TREE_LARGE  = this.AddGameObject("entity_tree_large", new Entity(new string[] { "LargeTree0" }, TILESHEET_ENTITY , new Vector2(-0.5f, -1f)));
            var ENTITY_TREE_STUMP  = this.AddGameObject("entity_tree_stump", new Entity(new string[] { "stump0", "stump1", "stump2" }, TILESHEET_ENTITY, new Vector2(0)));
            var ENTITY_ROCK        = this.AddGameObject("entity_rock", new Entity(new string[] { "Rock0", "Rock1", "Rock2", "Rock3" }, TILESHEET_ENTITY, new Vector2(0)));
            var ENTITY_PLANT       = this.AddGameObject("entity_plant", new Entity(new string[] { "Plant0", "Plant1", "Plant2", "Plant3", "Plant4", "Plant5" }, TILESHEET_ENTITY, new Vector2(0)));

            var ENTITY_CATUS       = this.AddGameObject("entity_Cactus", new Entity(new string[] { "Cactus1", "Cactus2" }, TILESHEET_ENTITY, new Vector2(0)));
            var ENTITY_CACTUS_TALE = this.AddGameObject("entity_cactus_tale", new Entity(new string[] { "Cactus0" }, TILESHEET_ENTITY, new Vector2(0, -1f)));

            var ENTITY_PLAYER      = this.AddGameObject("entity_player", new Creature(new PlayerAI(0, 1, 2, 3, 4, 5, 6, 7), new string[] { "Player_Move_Up", "Player_Move_Down", "Player_Move_Left", "Player_Move_Right", "Player_Idle_Up", "Player_Idle_Down", "Player_Idle_Left", "Player_Idle_Right" }, TILESHEET_CREATURES, new Vector2(0)));

            // Biomes
            // ----------------------------------------------------------------
            var BIOME_PLAINS = this.AddGameObject("biome_plains", new Biome(new PerlinDistribution(0.1f, 0),
                 new KeyWeightPair<int>[] {
                    new KeyWeightPair<int>(ENTITY_ROCK, 0.5),
                    new KeyWeightPair<int>(ENTITY_PLANT, 0.5)
                 },
                 new KeyWeightPair<int>[] {

                    new KeyWeightPair<int>(TILE_GRASS, 0.9),
                    new KeyWeightPair<int>(TILE_FLOWER, 0.1)
                 }));

            var BIOME_FOREST = this.AddGameObject("biome_forest", new Biome(new PerlinDistribution(0.1f, 0),
                new KeyWeightPair<int>[] {
                    new KeyWeightPair<int>(ENTITY_TREE_BIG, 0.3),
                    new KeyWeightPair<int>(ENTITY_TREE_PIN, 0.3),
                    new KeyWeightPair<int>(ENTITY_TREE_LARGE, 0.2),
                    new KeyWeightPair<int>(ENTITY_ROCK, 0.05),
                    new KeyWeightPair<int>(ENTITY_PLANT, 0.05),
                    new KeyWeightPair<int>(ENTITY_TREE_STUMP, 0.1)
                },
                new KeyWeightPair<int>[] {
                    new KeyWeightPair<int>(TILE_GRASS, 1)

                }));

            this.AddGameObject("biome_desert", new Biome(new FlatDistribution(0.1f),
                new KeyWeightPair<int>[] {
                    new KeyWeightPair<int>(ENTITY_CATUS, 0.75f),
                    new KeyWeightPair<int>(ENTITY_CACTUS_TALE, 0.25f)
                }, new KeyWeightPair<int>[] {
                    new KeyWeightPair<int>(TILE_SAND, 1)
                }));

            this.AddGameObject("biome_river", new Biome(new FlatDistribution(0),
                new KeyWeightPair<int>[] {

                }, new KeyWeightPair<int>[] {
                    new KeyWeightPair<int>(TILE_WATER, 1)
                }));

            this.AddGameObject("biome_ocean", new Biome(new FlatDistribution(0),
                new KeyWeightPair<int>[] {

                }, new KeyWeightPair<int>[] {
                    new KeyWeightPair<int>(TILE_WATER, 1)
                }));


            // Add event handle.
            GameEventHandler.OnWorldGeneratingEnd += GameEventHandler_OnWorldGenerating;
        }

        private void GameEventHandler_OnWorldGenerating(object sender, EventArgs e)
        {
            WorldEventArgs w = (WorldEventArgs)e;

            DataEntity E = new DataEntity(this.GetGameObjectIndex("entity_player"), 0);
            E.IsCameraFocus = true;

            w.World.playerEntity = E;

            w.World.RemoveEntityData(new WorldLocation(new Point(5, 5), new Point(5, 5)));
            w.World.AddEntityData(E, new WorldLocation(new Point(5, 5), new Point(5, 5)));
            w.World.Camera.FocusLocation = new WorldLocation(new Point(5, 5), new Point(5, 5));
        }
    }
}
