
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Maker.RiseEngine.Core.Audio
{
    public class SongManager
    {
        GameEngine Engine;

        bool isFading = false;
        bool isPlaying = false;

        string nextSong;
        string nextSongPluginName;

        float fadeVolume;
        
        public SongManager(GameEngine engine)
        {
            Engine = engine;
        }

        public  void SwitchSong(string songPlugin, string songName, bool repeating = true)
        {
            fadeVolume = 1f;
            isFading = true;
            nextSong = songName;
            nextSongPluginName = songPlugin;
            MediaPlayer.IsRepeating = repeating;
        }


        public  void Update(GameTime gameTime)
        {
            MediaPlayer.Volume = (((Engine.userConfig.SoundMasterLevel * Engine.userConfig.SoundSongLevel) / 2) * fadeVolume);

            if (isPlaying == true)
            {
                if (isFading == true)
                {
                    fadeVolume -= 0.01f;
                    if (fadeVolume <= 0.1)
                    {
                        MediaPlayer.Stop();
                        isPlaying = false;
                        fadeVolume = 1f;
                    }
                }
            }
            else if (!(nextSongPluginName == null || nextSong == null))
            {
                MediaPlayer.Play(Engine.ressourceManager.GetSong(nextSongPluginName, nextSong));
                isPlaying = true;
                isFading = false;
            }
        }
    }
}
