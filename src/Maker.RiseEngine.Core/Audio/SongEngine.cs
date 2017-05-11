
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Maker.RiseEngine.Core.Audio
{
    public class SongsManager
    {


        float FadeVolume;
        bool IsFading = false;
        bool Play = false;
        string NextSong;
        string PluginName;
        engine ENGINE;

        public SongsManager(engine engine) {
            ENGINE = engine;
        }

        public  void SwitchSong(string _PluginName, string _Name)
        {
            FadeVolume = 1f;
            IsFading = true;
            NextSong = _Name;
            PluginName = _PluginName;
            MediaPlayer.IsRepeating = true;
        }


        public  void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {

            MediaPlayer.Volume = (((rise.engineConfig.Sound_Master_Level * rise.engineConfig.Sound_Song_Level) / 2) * FadeVolume);

            if (Play == true)
            {
                if (IsFading == true)
                {

                    FadeVolume -= 0.01f;
                    if (FadeVolume <= 0.1)
                    {

                        MediaPlayer.Stop();
                        Play = false;
                        FadeVolume = 1f;

                    }
                }
            }
            else
            {

                if (!(PluginName == null || NextSong == null))
                {

                    MediaPlayer.Play(ENGINE.RESSOUCES.GetSong(PluginName, NextSong));
                    Play = true;
                    IsFading = false;

                }
            }
        }
    }
}
