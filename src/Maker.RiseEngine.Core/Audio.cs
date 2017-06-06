using Maker.RiseEngine.Storage;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

using System;
using System.Collections.Generic;
using System.IO;

namespace Maker.RiseEngine.Audio
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

        public void SwitchSong(string songPlugin, string songName, bool repeating = true)
        {
            fadeVolume = 1f;
            isFading = true;
            nextSong = songName;
            nextSongPluginName = songPlugin;
            MediaPlayer.IsRepeating = repeating;
        }


        public void Update(GameTime gameTime)
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

    public class SoundEffectManager
    {
        public int MaxPlayingSoundEffect;
        GameEngine Engine;

        List<SoundEffectInstance> soundEffectInstance;
        Random random;

        public SoundEffectManager(GameEngine engine, int maxPlayingSoundEffect)
        {
            Engine = engine;
            MaxPlayingSoundEffect = maxPlayingSoundEffect;
            soundEffectInstance = new List<SoundEffectInstance>();
            random = new Random();
        }

        public void PlaySoundEffect(SoundEffectColection soundEffectColection)
        {
            // Pick a random sound effect.
            int i = random.Next(soundEffectColection.SoundEffectList.Count);
            SoundEffect soundEffect = soundEffectColection.SoundEffectList[i];

            // Play sound effect.
            SoundEffectInstance newSoundEffectInstance = soundEffect.CreateInstance();
            soundEffectInstance.Add(newSoundEffectInstance);
            newSoundEffectInstance.Volume = (Engine.userConfig.SoundMasterLevel * Engine.userConfig.SoundEffectLevel);
            newSoundEffectInstance.Play();

        }

        public void Update(GameTime gameTime)
        {
            try
            {
                foreach (SoundEffectInstance i in soundEffectInstance)
                {
                    if (i.State == SoundState.Stopped)
                    {
                        soundEffectInstance.Remove(i);
                        i.Dispose();
                    }
                    else i.Volume = (Engine.userConfig.SoundMasterLevel * Engine.userConfig.SoundEffectLevel);
                }
            }
            catch (Exception) { }
        }

    }

    public class SoundEffectColection
    {
        public List<SoundEffect> SoundEffectList;

        public SoundEffectColection(GameEngine engine, string pluginName, string name)
        {

            SoundEffectList = new List<SoundEffect>();

            StreamReader sr = new StreamReader($"Plugins\\{pluginName}\\assets\\sounds_effects\\{name}.rise");
            string f = sr.ReadToEnd().ToDosLineEnd();
            sr.Close();

            f = f.Replace(Environment.NewLine, "");
            string[] Ls = f.Split(',');


            for (int i = 0; i < Ls.Length; i++)
            {

                SoundEffectList.Add(engine.ressourceManager.GetSoundEffect(pluginName, Ls[i]));

            }

        }
    }
}
