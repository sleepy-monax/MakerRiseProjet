using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.Audio
{
    public static class SongEngine 
    {

        
        static float FadeVolume;
        static bool IsFading = false;
        static bool Play = false;
        static string NextSong;
        static string PluginName;

        public static void SwitchSong(string _PluginName,string _Name) {
            FadeVolume = 1f;
            IsFading = true;
            NextSong = _Name;
            PluginName = _PluginName;
            MediaPlayer.IsRepeating = true;
        } 

        

        public static void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime) {
            MediaPlayer.Volume = (((Config.Sound.Master * Config.Sound.Songs) / 2) * FadeVolume);

            if (Play == true) {

                if (IsFading == true) {

                    FadeVolume -= 0.01f;
                    if (FadeVolume <= 0.1) {

                        MediaPlayer.Stop();
                        Play = false;
                        FadeVolume = 1f;

                    }

                }
                

            } else {
                MediaPlayer.Play(ContentEngine.Song(PluginName, NextSong));
                Play = true;
                IsFading = false;
            }

        }

    }
}
