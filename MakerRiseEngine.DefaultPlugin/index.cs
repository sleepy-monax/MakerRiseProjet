using Microsoft.Xna.Framework;
using RiseEngine.Core;
using RiseEngine.Core.Audio;
using RiseEngine.Core.GameMath;
using RiseEngine.Core.Rendering.SpriteSheets;
using RiseEngine.Core.World;
using RiseEngine.Core.World.Obj;
using RiseEngine.Core.World.Utils;

namespace RiseEngine.DefaultPlugin
{
    class Plugin : Core.Plugin.IPlugin
    {
        public string Name
        {
            get
            {
                return "Base";
            }
        }

        public void Initialize()
        {

            //TileSheet
            GameObjectsManager.SpriteSheets.Add("Tilesheet_Terrain", new SpriteSheet("Base", ContentEngine.Texture2D("Base", "Tilesheet_Terrain"), "Tilesheet_Terrain", new Point(32)));
            GameObjectsManager.SpriteSheets.Add("Tilesheet_Entity", new SpriteSheet("Base", ContentEngine.Texture2D("Base", "Tilesheet_Entity"), "Tilesheet_Entity", new Point(16)));
            GameObjectsManager.SpriteSheets.Add("Tilesheet_Item", new SpriteSheet("Base", ContentEngine.Texture2D("Base", "Tilesheet_Item"), "Tilesheet_Item", new Point(16)));
            GameObjectsManager.SpriteSheets.Add("Tilesheet_Creatures", new SpriteSheet("Base", ContentEngine.Texture2D("Base", "Tilesheet_Creatures"), "Tilesheet_Creatures", new Point(16)));
            GameObjectsManager.SpriteSheets.Add("Tilesheet_Water", new SpriteSheet("Base", ContentEngine.Texture2D("Base", "Tilesheet_Water"), "Tilesheet_Water", new Point(32)));

            //Items
            GameObjectsManager.AddItem(this, "Stick", new Core.GameObject.Items.Item("Stick", Core.GameObject.ItemType.Crafting, new string[] { "Stick" }, "Tilesheet_Item"));
            GameObjectsManager.AddItem(this, "Apple", new Core.GameObject.Items.Item("Apple", Core.GameObject.ItemType.Food, new string[] { "AppleRed", "AppleGreen", "AppleYellow" }, "Tilesheet_Item"));

            //Tiles
            Core.GameObject.Tiles.Tile Grass = new Core.GameObject.Tiles.Tile("Grass", new string[] { "Grass0", "Grass1", "Grass2", "Grass3" }, "Tilesheet_Terrain", System.Drawing.Color.FromArgb(36, 81, 11));
            Grass.SetSouneffect(SoundEffectParser.Parse(this.Name, "dirtfootstep"));
            GameObjectsManager.AddTile(this, "Grass", Grass);


            GameObjectsManager.AddTile(this, "FlowerOnGrass", new Core.GameObject.Tiles.Tile("FlowerOnGrass", new string[] { "YellowFlowerGrass", "PurpleFlowerGrass" }, "Tilesheet_Terrain", System.Drawing.Color.FromArgb(36, 81, 11)));
            GameObjectsManager.AddTile(this, "Sand", new Core.GameObject.Tiles.Tile("Sand", new string[] { "Sand0", "Sand1", "Sand2", "Sand3" }, "Tilesheet_Terrain", System.Drawing.Color.Yellow));
            GameObjectsManager.AddTile(this, "Water", new Core.GameObject.Tiles.Tile("Water", new string[] { "Water" }, "Tilesheet_Water", System.Drawing.Color.Blue));

            //Entity

            GameObjectsManager.AddEntity(this, "BigTree", new Core.GameObject.Entities.Entity("BigTree", new string[] { "BigTree0" }, "Tilesheet_Entity", new Vector2(-0.5f, -3f)));
            GameObjectsManager.AddEntity(this, "PinTree", new Core.GameObject.Entities.Entity("PinTree", new string[] { "PinTree0" }, "Tilesheet_Entity", new Vector2(-0.5f, -3f)));
            GameObjectsManager.AddEntity(this, "LargeTree", new Core.GameObject.Entities.Entity("LargeTree", new string[] { "LargeTree0" }, "Tilesheet_Entity", new Vector2(-0.5f, -1f)));
            GameObjectsManager.AddEntity(this, "Rock", new Core.GameObject.Entities.Entity("Rock", new string[] { "Rock0", "Rock1", "Rock2", "Rock3" }, "Tilesheet_Entity", new Vector2(0, 0)));
            GameObjectsManager.AddEntity(this, "Plant", new Core.GameObject.Entities.Entity("Plant", new string[] { "Plant0", "Plant1", "Plant2", "Plant3", "Plant4", "Plant5" }, "Tilesheet_Entity", new Vector2(0, 0)));
            GameObjectsManager.AddEntity(this, "Player", new Core.GameObject.Entities.Creature("Player", new Core.IA.Player(0, 1, 2, 3, 4), new string[] { "Player_Move_Up", "Player_Move_Down", "Player_Move_Left", "Player_Move_Right", "Player_Idle" }, "Tilesheet_Creatures", new Vector2(0, 0)));

            //Biome
            GameObjectsManager.AddBiome(this, "Plains", new Core.GameObject.Biome("Plains", 0.1,
                new KeyWeightPair<int>[] {
                    new KeyWeightPair<int>(GameObjectsManager.EntityKey["Base.Rock"], 0.5),
                    new KeyWeightPair<int>(GameObjectsManager.EntityKey["Base.Plant"], 0.5)
                },
                new KeyWeightPair<int>[] {

                    new KeyWeightPair<int>(GameObjectsManager.TileKeys["Base.Grass"], 0.9),
                    new KeyWeightPair<int>(GameObjectsManager.TileKeys["Base.FlowerOnGrass"], 0.1)
                }));

            GameObjectsManager.AddBiome(this, "Forest", new Core.GameObject.Biome("Forest", 0.3,
                new KeyWeightPair<int>[] {
                    new KeyWeightPair<int>(GameObjectsManager.EntityKey["Base.BigTree"], 0.3),
                    new KeyWeightPair<int>(GameObjectsManager.EntityKey["Base.PinTree"], 0.3),
                    new KeyWeightPair<int>(GameObjectsManager.EntityKey["Base.LargeTree"], 0.2),
                    new KeyWeightPair<int>(GameObjectsManager.EntityKey["Base.Rock"], 0.1),
                    new KeyWeightPair<int>(GameObjectsManager.EntityKey["Base.Plant"], 0.1),
                },
                new KeyWeightPair<int>[] {
                    new KeyWeightPair<int>(GameObjectsManager.TileKeys["Base.Grass"], 1)

                }));

            GameObjectsManager.AddBiome(this, "Ocean", new Core.GameObject.Biome("Ocean", 0, new KeyWeightPair<int>[] { }, new KeyWeightPair<int>[] { new KeyWeightPair<int>(GameObjectsManager.TileKeys["Base.Water"], 1) }));
            GameObjectsManager.AddBiome(this, "Desert", new Core.GameObject.Biome("Desert", 0, new KeyWeightPair<int>[] { }, new KeyWeightPair<int>[] { new KeyWeightPair<int>(GameObjectsManager.TileKeys["Base.Sand"], 1) }));

        }

        public void OnWorldGeneration(WorldScene world)
        {

            ObjEntity E = new ObjEntity(GameObjectsManager.EntityKey["Base.Player"], 0);
            E.IsFocus = true;

            world.entityManager.RemoveEntity(new WorldLocation(new Point(5, 5), new Point(5, 5)));
            world.entityManager.AddEntity(E, new WorldLocation(new Point(5, 5), new Point(5, 5)));
            world.Camera.FocusLocation = new WorldLocation(new Point(5, 5), new Point(5, 5)).ToPoint();
            world.Camera.Update();

        }
    }
}
