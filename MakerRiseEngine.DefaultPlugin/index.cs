using Microsoft.Xna.Framework;
using RiseEngine.Core;
using RiseEngine.Core.Audio;
using RiseEngine.Core.GameMath;
using RiseEngine.Core.Rendering.SpriteSheets;
using RiseEngine.Core.World;
using RiseEngine.Core.World.WorldObj;
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
            this.AddGameObject("Move", new Core.AI.Action.Move());

            //TileSheet
            this.AddGameObject("Tilesheet_Terrain", new SpriteSheet("Base", ContentEngine.Texture2D("Base", "Tilesheet_Terrain"), "Tilesheet_Terrain", new Point(32)));
            this.AddGameObject("Tilesheet_Entity", new SpriteSheet("Base", ContentEngine.Texture2D("Base", "Tilesheet_Entity"), "Tilesheet_Entity", new Point(16)));
            this.AddGameObject("Tilesheet_Item", new SpriteSheet("Base", ContentEngine.Texture2D("Base", "Tilesheet_Item"), "Tilesheet_Item", new Point(16)));
            this.AddGameObject("Tilesheet_Creatures", new SpriteSheet("Base", ContentEngine.Texture2D("Base", "Tilesheet_Creatures"), "Tilesheet_Creatures", new Point(16)));
            this.AddGameObject("Tilesheet_Water", new SpriteSheet("Base", ContentEngine.Texture2D("Base", "Tilesheet_Water"), "Tilesheet_Water", new Point(32)));

            //Items
            this.AddGameObject("Stick", new Core.GameObject.Items.Item(Core.GameObject.ItemType.Crafting, new string[] { "Stick" }, "Base.Tilesheet_Item"));
            this.AddGameObject("Apple", new Core.GameObject.Items.Item(Core.GameObject.ItemType.Food, new string[] { "AppleRed", "AppleGreen", "AppleYellow" }, "Base.Tilesheet_Item"));

            //Tiles
            Core.GameObject.Tiles.Tile Grass = new Core.GameObject.Tiles.Tile(new string[] { "Grass0", "Grass1", "Grass2", "Grass3" }, "Base.Tilesheet_Terrain", System.Drawing.Color.FromArgb(36, 81, 11));
            Grass.SetSoundEffect(SoundEffectParser.Parse(this.Name, "dirtfootstep"));
            this.AddGameObject("Grass", Grass);


            this.AddGameObject("FlowerOnGrass", new Core.GameObject.Tiles.Tile(new string[] { "YellowFlowerGrass", "PurpleFlowerGrass" }, "Base.Tilesheet_Terrain", System.Drawing.Color.FromArgb(36, 81, 11)) { });
            this.AddGameObject("Sand", new Core.GameObject.Tiles.Tile(new string[] { "Sand0", "Sand1", "Sand2", "Sand3" }, "Base.Tilesheet_Terrain", System.Drawing.Color.Yellow));
            this.AddGameObject("Water", new Core.GameObject.Tiles.Tile(new string[] { "Water" }, "Base.Tilesheet_Water", System.Drawing.Color.Blue));

            //Entity

            this.AddGameObject("BigTree", new Core.GameObject.Entities.Entity(new string[] { "BigTree0" }, "Base.Tilesheet_Entity", new Vector2(-0.5f, -3f)));
            this.AddGameObject("PinTree", new Core.GameObject.Entities.Entity(new string[] { "PinTree0" }, "Base.Tilesheet_Entity", new Vector2(-0.5f, -3f)));
            this.AddGameObject("LargeTree", new Core.GameObject.Entities.Entity(new string[] { "LargeTree0" }, "Base.Tilesheet_Entity", new Vector2(-0.5f, -1f)));
            this.AddGameObject("Rock", new Core.GameObject.Entities.Entity(new string[] { "Rock0", "Rock1", "Rock2", "Rock3" }, "Base.Tilesheet_Entity", new Vector2(0, 0)));
            this.AddGameObject("Plant", new Core.GameObject.Entities.Entity(new string[] { "Plant0", "Plant1", "Plant2", "Plant3", "Plant4", "Plant5" }, "Base.Tilesheet_Entity", new Vector2(0, 0)));
            this.AddGameObject("Player", new Core.GameObject.Entities.Creature(new Core.AI.Entites.Player(0, 1, 2, 3, 4,5,6,7), new string[] { "Player_Move_Up", "Player_Move_Down", "Player_Move_Left", "Player_Move_Right", "Player_Idle_Up","Player_Idle_Down","Player_Idle_Left","Player_Idle_Right"}, "Base.Tilesheet_Creatures", new Vector2(0, 0)));

            //Biome
            this.AddGameObject("Plains", new Core.GameObject.Biome(0.1,
                 new KeyWeightPair<int>[] {
                    new KeyWeightPair<int>(this.GetGameObjectIndex("Rock"), 0.5),
                    new KeyWeightPair<int>(this.GetGameObjectIndex("Plant"), 0.5)
                 },
                 new KeyWeightPair<int>[] {

                    new KeyWeightPair<int>(this.GetGameObjectIndex("Grass"), 0.9),
                    new KeyWeightPair<int>(this.GetGameObjectIndex("FlowerOnGrass"), 0.1)
                 }));



            this.AddGameObject("Forest", new Core.GameObject.Biome(0.3,
                new KeyWeightPair<int>[] {
                    new KeyWeightPair<int>(this.GetGameObjectIndex("BigTree"), 0.3),
                    new KeyWeightPair<int>(this.GetGameObjectIndex("PinTree"), 0.3),
                    new KeyWeightPair<int>(this.GetGameObjectIndex("LargeTree"), 0.2),
                    new KeyWeightPair<int>(this.GetGameObjectIndex("Rock"), 0.1),
                    new KeyWeightPair<int>(this.GetGameObjectIndex("Plant"), 0.1),
                },
                new KeyWeightPair<int>[] {
                    new KeyWeightPair<int>(this.GetGameObjectIndex("Grass"), 1)

                }));

            this.AddGameObject("Ocean", new Core.GameObject.Biome(0, new KeyWeightPair<int>[] { }, new KeyWeightPair<int>[] { new KeyWeightPair<int>(this.GetGameObjectIndex("Water"), 1) }));
            this.AddGameObject("Desert", new Core.GameObject.Biome(0, new KeyWeightPair<int>[] { }, new KeyWeightPair<int>[] { new KeyWeightPair<int>(this.GetGameObjectIndex("Sand"), 1) }));

        }

        public void OnWorldGeneration(WorldScene world)
        {

            ObjEntity E = new ObjEntity(this.GetGameObjectIndex("Player"), 0);
            E.IsFocus = true;

            world.entityManager.RemoveEntity(new WorldLocation(new Point(5, 5), new Point(5, 5)));
            world.entityManager.AddEntity(E, new WorldLocation(new Point(5, 5), new Point(5, 5)));
            world.Camera.FocusLocation = new WorldLocation(new Point(5, 5), new Point(5, 5)).ToPoint();
            world.Camera.Update();

        }
    }
}
