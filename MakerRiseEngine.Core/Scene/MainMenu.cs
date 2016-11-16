using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.World;
using System;
using System.Threading;
using System.Windows.Forms;
using static Maker.RiseEngine.Core.Rendering.SpriteFontDraw;

namespace Maker.RiseEngine.Core.Scene
{
    public class MainMenu : Idrawable
    {

        #region MainMenu

        private SpriteBatch BackgroundSB;
        private Rendering.Parallax Background;

        private UI.ContainerManager MainMenuManager;

        //Main Menu
        private UI.Container MenuContainer;
        private UI.Controls.Button MenuButtonPlay;
        private UI.Controls.Button MenuButtonOpti;
        private UI.Controls.Button MenuButtonQuit;
        private Texture2D Logo;
        private Texture2D MakerLogo;

        //Options
        public UI.Container OptionContainer;
        private UI.Controls.Button OptionGameButton;
        private UI.Controls.Button OptionInputButton;
        private UI.Controls.Button OptionOtherButton;
        private UI.Controls.Button OptionSoundButton;
        private UI.Controls.Button OptionGraphButton;
        private UI.Controls.Button OptionApplyButton;
        public UI.Controls.Button OptionBackButton;

        //WorldManager
        private UI.Container WrlMngrContainer;
        private UI.Controls.Button MngrDoneButton;
        private UI.Controls.Button MngrLoadButton;
        private UI.Controls.Button MngrNewButton;
        private UI.Controls.Button MngrDelButton;
        private UI.Controls.Label MngrLabel;

        //NewWorld
        private UI.Container NewWrldContainer;
        private UI.Controls.Button NewWrldCreateButton;
        private UI.Controls.Button NewWrldCancelButton;
        private UI.Controls.TextBox NewWrldNameTextBox;
        private UI.Controls.TextBox NewWrldSeedTextBox;
        private UI.Controls.Label NewWrldTitleLabel;
        private UI.Controls.Label NewWrldSeedlabel;
        private UI.Controls.Label NewWrldNameLabel;
        private UI.Controls.CheckBox NewWrldStorieMode;
        private UI.Controls.CheckBox NewWrldCheat;

        //MultiPlayer
        #endregion

        #region OptionPanel

        public UI.ContainerManager OptionMenuManager;

        private UI.Container GameOptionContainer;
        private UI.Controls.Label GameOptionTitle;

        private UI.Container SoundOptionContainer;
        private UI.Controls.Label SoundOptionTitle;
        private UI.Controls.Label SoundOptionMasterLabel;
        private UI.Controls.Slider SoundOptionMasterSlider;

        private UI.Container PerfOptionContainer;
        private UI.Controls.Label PerfOptionTitle;

        private UI.Container InputOptionContainer;
        private UI.Controls.Label InputOptionTitle;

        private UI.Container OtherOptionContainer;
        private UI.Controls.Label OtherOptionTitle;
        private UI.Controls.CheckBox OtherOptionDebugFrameCounter;
        private UI.Controls.CheckBox OtherOptionDebugGuiFrame;
        private UI.Controls.CheckBox OtherOptionDebugWorldOverDraw;
        private UI.Controls.CheckBox OtherOptionDebugWorldFocusLocation;
        private UI.Controls.CheckBox OtherOptionDebugWaterMark;




        #endregion


        public MainMenu()
        {

            Logo = ContentEngine.Texture2D("Engine", "Logo");
            MakerLogo = ContentEngine.Texture2D("Engine", "MakerLogo");

            BackgroundSB = new SpriteBatch(Common.GraphicsDevice);
            switch (new Random().Next(3))
            {
                case 0:
                    Background = Rendering.ParallaxParse.Parse("Engine", "Dusk Mountain", new Rectangle(0, 0, Common.graphics.PreferredBackBufferWidth, Common.graphics.PreferredBackBufferHeight));
                    Core.Audio.SongEngine.SwitchSong("Engine", "A Title");
                    break;
                case 1:
                    Background = Rendering.ParallaxParse.Parse("Engine", "Forest", new Rectangle(0, 0, Common.graphics.PreferredBackBufferWidth, Common.graphics.PreferredBackBufferHeight));
                    Core.Audio.SongEngine.SwitchSong("Engine", "Look Up");
                    break;
                case 2:
                    Background = Rendering.ParallaxParse.Parse("Engine", "Void", new Rectangle(0, 0, Common.graphics.PreferredBackBufferWidth, Common.graphics.PreferredBackBufferHeight));
                    Core.Audio.SongEngine.SwitchSong("Engine", "Clouds of Orange Juice");
                    break;
                default:
                    break;
            }



            

            MainMenuManager = new UI.ContainerManager();

            //Main Menu

            MenuContainer = new UI.Container(-256, -120, 512, 240, false, UI.Dock.Center, Color.White);

            MenuButtonPlay = new UI.Controls.Button("Jouer", 480, 16, 16, Color.White);
            MenuButtonOpti = new UI.Controls.Button("Option", 480, 16, 80, Color.White);
            MenuButtonQuit = new UI.Controls.Button("Quitter", 480, 16, 160, Color.White);

            MenuButtonPlay.OnMouseClick += new UI.Control.ClickEventHandler(this.playGame);
            MenuButtonOpti.OnMouseClick += new UI.Control.ClickEventHandler(this.ShowOption);
            MenuButtonQuit.OnMouseClick += new UI.Control.ClickEventHandler(this.ExitGame);

            MenuContainer.Controls.Add(MenuButtonPlay);
            MenuContainer.Controls.Add(MenuButtonOpti);
            MenuContainer.Controls.Add(MenuButtonQuit);

            //Option

            OptionContainer = new UI.Container(16, 16, 288, Common.graphics.PreferredBackBufferHeight - 32, false, UI.Dock.UpLeft, Color.White)
            {
                ShowTitle = true,
                Title = "Option"

            };



            OptionGameButton = new UI.Controls.Button("Jeux", 256, 16, 96, Color.White);
            OptionSoundButton = new UI.Controls.Button("Sons", 256, 16, 160, Color.White);
            OptionGraphButton = new UI.Controls.Button("Performances", 256, 16, 224, Color.White);
            OptionInputButton = new UI.Controls.Button("Commandes", 256, 16, 288, Color.White);
            OptionOtherButton = new UI.Controls.Button("Autre", 256, 16, 416, Color.White);
            OptionApplyButton = new UI.Controls.Button("Appliquer", 256, 16, OptionContainer.ContainerRect.Height - 64 - 16 - 16 - 64, Color.White);
            OptionBackButton = new UI.Controls.Button("Retour", 256, 16, OptionContainer.ContainerRect.Height - 64 - 16, Color.White);

            OptionGameButton.OnMouseClick += new UI.Control.ClickEventHandler(this.SwitchOptMenu_Game);
            OptionSoundButton.OnMouseClick += new UI.Control.ClickEventHandler(this.SwitchOptMenu_Sound);
            OptionGraphButton.OnMouseClick += new UI.Control.ClickEventHandler(this.SwitchOptMenu_Gfx);
            OptionInputButton.OnMouseClick += new UI.Control.ClickEventHandler(this.SwitchOptMenu_Input);
            OptionOtherButton.OnMouseClick += new UI.Control.ClickEventHandler(this.SwitchOptMenu_Other);
            OptionApplyButton.OnMouseClick += new UI.Control.ClickEventHandler(this.Apply);
            OptionBackButton.OnMouseClick += new UI.Control.ClickEventHandler(this.GoBackToMain);

            OptionContainer.Controls.Add(OptionBackButton);
            OptionContainer.Controls.Add(OptionOtherButton);
            OptionContainer.Controls.Add(OptionGameButton);
            OptionContainer.Controls.Add(OptionGraphButton);
            OptionContainer.Controls.Add(OptionInputButton);
            OptionContainer.Controls.Add(OptionApplyButton);
            OptionContainer.Controls.Add(OptionSoundButton);

            #region OptionPanel
            //=======================================================================================================================================
            OptionMenuManager = new UI.ContainerManager();



            //=====Game=====
            GameOptionContainer = new UI.Container(new Rectangle(320, 16, Common.graphics.PreferredBackBufferWidth - 336, Common.graphics.PreferredBackBufferHeight - 32), true, UI.Dock.UpLeft, Color.White);
            GameOptionTitle = new UI.Controls.Label("Jeux", Common.graphics.PreferredBackBufferWidth - 304, 0, 16, Alignment.Center, Style.DropShadow, Color.White);

            GameOptionContainer.Controls.Add(GameOptionTitle);

            OptionMenuManager.AddContainer("Game", GameOptionContainer);

            //=====sound=====
            SoundOptionContainer = new UI.Container(new Rectangle(320, 16, Common.graphics.PreferredBackBufferWidth - 336, Common.graphics.PreferredBackBufferHeight - 32), true, UI.Dock.UpLeft, Color.White);
            SoundOptionTitle = new UI.Controls.Label("Music et sons", SoundOptionContainer.ContainerRect.Width, 0, 16, Alignment.Center, Style.DropShadow, Color.White);
            SoundOptionMasterLabel = new UI.Controls.Label("Volume Principale", SoundOptionContainer.ContainerRect.Width - 64, 32, 96, Alignment.Left, Style.Regular, Color.White);
            SoundOptionMasterSlider = new UI.Controls.Slider(32, 160, SoundOptionContainer.ContainerRect.Width - 64);

            SoundOptionContainer.Controls.Add(SoundOptionTitle);
            SoundOptionContainer.Controls.Add(SoundOptionMasterLabel);
            SoundOptionContainer.Controls.Add(SoundOptionMasterSlider);

            OptionMenuManager.AddContainer("Sound", SoundOptionContainer);

            //=====Perf=====
            PerfOptionContainer = new UI.Container(new Rectangle(320, 16, Common.graphics.PreferredBackBufferWidth - 336, Common.graphics.PreferredBackBufferHeight - 32), true, UI.Dock.UpLeft, Color.White);
            PerfOptionTitle = new UI.Controls.Label("Graphisme", PerfOptionContainer.ContainerRect.Width, 0, 16, Alignment.Center, Style.DropShadow, Color.White);

            PerfOptionContainer.Controls.Add(PerfOptionTitle);

            OptionMenuManager.AddContainer("Gfx", PerfOptionContainer);

            //=====Inputs=====
            InputOptionContainer = new UI.Container(new Rectangle(320, 16, Common.graphics.PreferredBackBufferWidth - 336, Common.graphics.PreferredBackBufferHeight - 32), true, UI.Dock.UpLeft, Color.White);
            InputOptionTitle = new UI.Controls.Label("Commande", InputOptionContainer.ContainerRect.Width, 0, 16, Alignment.Center, Style.DropShadow, Color.White);

            InputOptionContainer.Controls.Add(InputOptionTitle);

            OptionMenuManager.AddContainer("Input", InputOptionContainer);


            //=====Other=====
            OtherOptionContainer = new UI.Container(new Rectangle(320, 16, Common.graphics.PreferredBackBufferWidth - 336, Common.graphics.PreferredBackBufferHeight - 32), true, UI.Dock.UpLeft, Color.White);
            OtherOptionTitle = new UI.Controls.Label("Autre", OtherOptionContainer.ContainerRect.Width, 0, 16, Alignment.Center, Style.DropShadow, Color.White);

            OtherOptionDebugFrameCounter = new UI.Controls.CheckBox("Conteur d'ips", Config.Debug.FrameCounter, 256, 16, 96);
            OtherOptionDebugGuiFrame = new UI.Controls.CheckBox("Contours des interfaces", Config.Debug.GuiFrame, 256, 16, 176);
            OtherOptionDebugWorldOverDraw = new UI.Controls.CheckBox("Limites des Chunks", Config.Debug.WorldOverDraw, 256, 16, 256);
            OtherOptionDebugWorldFocusLocation = new UI.Controls.CheckBox("Afficher l'objet central de la camera", Config.Debug.WorldFocusLocation, 256, 16, 336);
            OtherOptionDebugWaterMark = new UI.Controls.CheckBox("Information Avancées", Config.Debug.DebugWaterMark, 256, 16, 416);


            OtherOptionContainer.Controls.Add(OtherOptionTitle);
            OtherOptionContainer.Controls.Add(OtherOptionDebugFrameCounter);
            OtherOptionContainer.Controls.Add(OtherOptionDebugGuiFrame);
            OtherOptionContainer.Controls.Add(OtherOptionDebugWorldOverDraw);
            OtherOptionContainer.Controls.Add(OtherOptionDebugWorldFocusLocation);
            OtherOptionContainer.Controls.Add(OtherOptionDebugWaterMark);

            OptionMenuManager.AddContainer("Other", OtherOptionContainer);

            OptionMenuManager.SwitchContainer("Game");

            //=======================================================================================================================================
            #endregion

            //World Manager

            WrlMngrContainer = new UI.Container(Common.graphics.PreferredBackBufferWidth / 2 - 512, 64, 1024, Common.graphics.PreferredBackBufferHeight - 128, true, UI.Dock.UpLeft, Color.White);

            MngrLoadButton = new UI.Controls.Button("Charger", 330, 16, WrlMngrContainer.ContainerRect.Height - 160 + 16, Color.White);
            MngrNewButton = new UI.Controls.Button("Nouveau", 330, 347, WrlMngrContainer.ContainerRect.Height - 160 + 16, Color.White);
            MngrDelButton = new UI.Controls.Button("Supprimer", 330, 678, WrlMngrContainer.ContainerRect.Height - 160 + 16, Color.White);
            MngrDoneButton = new UI.Controls.Button("Retour", WrlMngrContainer.ContainerRect.Width - 32, 16, WrlMngrContainer.ContainerRect.Height - 64 - 16, Color.White);
            MngrLabel = new UI.Controls.Label("Mes Mondes", 320, WrlMngrContainer.ContainerRect.Width / 2 - 160, 16, Alignment.Center, Style.DropShadow, Color.White);


            MngrDoneButton.OnMouseClick += new UI.Control.ClickEventHandler(this.GoBackToMain);
            MngrNewButton.OnMouseClick += new UI.Control.ClickEventHandler(this.ShowCreateWorld);

            WrlMngrContainer.Controls.Add(MngrLoadButton);
            WrlMngrContainer.Controls.Add(MngrNewButton);
            WrlMngrContainer.Controls.Add(MngrDelButton);
            WrlMngrContainer.Controls.Add(MngrDoneButton);
            WrlMngrContainer.Controls.Add(MngrLabel);

            //new World

            NewWrldContainer = new UI.Container(Common.graphics.PreferredBackBufferWidth / 2 - 512, 64, 1024, Common.graphics.PreferredBackBufferHeight - 128, true, UI.Dock.UpLeft, Color.White);

            NewWrldTitleLabel = new UI.Controls.Label("Créer Un Nouveau Monde", 320, NewWrldContainer.ContainerRect.Width / 2 - 160, 16, Alignment.Center, Style.DropShadow, Color.White);

            NewWrldNameLabel = new UI.Controls.Label("Nom du monde", 800, NewWrldContainer.ContainerRect.Width / 2 - 400, 96, Alignment.Left, Style.Regular, Color.White);
            NewWrldNameTextBox = new UI.Controls.TextBox("Monde", 64, NewWrldContainer.ContainerRect.Width / 2 - 400, 160);

            NewWrldSeedlabel = new UI.Controls.Label("Graine pour la génération du monde", 800, NewWrldContainer.ContainerRect.Width / 2 - 400, 240, Alignment.Left, Style.Regular, Color.White);
            NewWrldSeedTextBox = new UI.Controls.TextBox("123456789", 64, NewWrldContainer.ContainerRect.Width / 2 - 400, 304); //800px

            NewWrldCreateButton = new UI.Controls.Button("Créer le nouveau Monde", 496, 16, NewWrldContainer.ContainerRect.Height - 64 - 16, Color.White);
            NewWrldCancelButton = new UI.Controls.Button("Retour", 496, 512, NewWrldContainer.ContainerRect.Height - 64 - 16, Color.White);

            NewWrldStorieMode = new UI.Controls.CheckBox("Créer une histoire", true, 496, 16, NewWrldContainer.ContainerRect.Height - 128 - 16);
            NewWrldCheat = new UI.Controls.CheckBox("Activé la triche", true, 496, 512, NewWrldContainer.ContainerRect.Height - 128 - 16);

            NewWrldCancelButton.OnMouseClick += new UI.Control.ClickEventHandler(this.GoBackToWorldManager);
            NewWrldCreateButton.OnMouseClick += new UI.Control.ClickEventHandler(this.CreateWorld);

            NewWrldContainer.Controls.Add(NewWrldTitleLabel);
            NewWrldContainer.Controls.Add(NewWrldSeedlabel);
            NewWrldContainer.Controls.Add(NewWrldNameLabel);
            NewWrldContainer.Controls.Add(NewWrldSeedTextBox);
            NewWrldContainer.Controls.Add(NewWrldNameTextBox);
            NewWrldContainer.Controls.Add(NewWrldCreateButton);
            NewWrldContainer.Controls.Add(NewWrldCancelButton);

            NewWrldContainer.Controls.Add(NewWrldCheat);
            NewWrldContainer.Controls.Add(NewWrldStorieMode);

            //intialize Container Manager
            MainMenuManager.AddContainer("MainMenu", MenuContainer);
            MainMenuManager.AddContainer("Option", OptionContainer);
            MainMenuManager.AddContainer("WorldManager", WrlMngrContainer);
            MainMenuManager.AddContainer("NewWorld", NewWrldContainer);

            MainMenuManager.SwitchContainer("MainMenu");

        }

        #region MainMenu
        private void playGame()
        {
            MainMenuManager.SwitchContainer("WorldManager");
        }

        private void ShowOption()
        {
            MainMenuManager.SwitchContainer("Option");
        }

        private void ExitGame() { Application.Exit(); }
        #endregion

        #region OptionMenu

        private void SwitchOptMenu_Game()
        {
            OptionMenuManager.SwitchContainer("Game");
        }

        private void SwitchOptMenu_Sound()
        {
            OptionMenuManager.SwitchContainer("Sound");
        }

        private void SwitchOptMenu_Gfx()
        {
            OptionMenuManager.SwitchContainer("Gfx");
        }

        private void SwitchOptMenu_Input()
        {
            OptionMenuManager.SwitchContainer("Input");
        }

        private void SwitchOptMenu_Other()
        {
            OptionMenuManager.SwitchContainer("Other");
        }

        private void GoBackToMain()
        {
            MainMenuManager.SwitchContainer("MainMenu");
        }

        private void SwitchOptMenu_Ext()
        {

            OptionMenuManager.SwitchContainer("Ext");

        }

        #endregion

        #region WorldManager

        private void ShowCreateWorld()
        {
            MainMenuManager.SwitchContainer("NewWorld");
        }


        #endregion

        #region OptionPanel

        private void Apply()
        {

            //Other

            Config.Debug.FrameCounter = OtherOptionDebugFrameCounter.IsChecked;
            Config.Debug.GuiFrame = OtherOptionDebugGuiFrame.IsChecked;
            Config.Debug.WorldFocusLocation = OtherOptionDebugWorldFocusLocation.IsChecked;
            Config.Debug.WorldOverDraw = OtherOptionDebugWorldOverDraw.IsChecked;
            Config.Debug.DebugWaterMark = OtherOptionDebugWaterMark.IsChecked;

        }

        #endregion

        #region NewWorld

        private void GoBackToWorldManager()
        {
            MainMenuManager.SwitchContainer("WorldManager");
        }

        private void CreateWorld()
        {

            ThreadStart GenHandle = new ThreadStart(delegate
            {
                World.Utils.WorldProperty wrldp = new World.Utils.WorldProperty()
                {
                    WorldName = NewWrldNameTextBox.Text,
                    Seed = int.Parse(NewWrldSeedTextBox.Text)
                };

                Generator.WorldGenerator Gen = new Generator.WorldGenerator(wrldp);
                WorldScene wrldsc = Gen.Generate();

                SceneManager.StartGame(wrldsc);
            });

            SceneManager.CurrentScene = 4;

            Thread t = new Thread(GenHandle);
            t.Start();

        }

        #endregion


        public void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {
            Background.Update(Mouse, KeyBoard, gameTime);
            MainMenuManager.Update(Mouse, KeyBoard, gameTime);

            if (MainMenuManager.CurrentContainerKey == "Option")
            {
                OptionMenuManager.Update(Mouse, KeyBoard, gameTime);
            }

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            BackgroundSB.Begin();
            Background.Draw(BackgroundSB, gameTime);
            BackgroundSB.End();

            spriteBatch.FillRectangle(new Rectangle(0, 0, Common.graphics.PreferredBackBufferWidth, Common.graphics.PreferredBackBufferHeight), new Color(0, 0, 0, 100));

            spriteBatch.Draw(Logo, new Vector2(Common.graphics.PreferredBackBufferWidth / 2 - Logo.Width / 2 + 2, Common.graphics.PreferredBackBufferHeight / 2 - Logo.Height / 2 - 230 + 2), new Color(0, 0, 0, 125));
            spriteBatch.Draw(Logo, new Vector2(Common.graphics.PreferredBackBufferWidth / 2 - Logo.Width / 2, Common.graphics.PreferredBackBufferHeight / 2 - Logo.Height / 2 - 230));


            MainMenuManager.Draw(spriteBatch, gameTime);

            if (MainMenuManager.CurrentContainerKey == "Option")
            {
                OptionMenuManager.Draw(spriteBatch, gameTime);
            }

           
        }
    }
}
