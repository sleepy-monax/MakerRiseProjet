using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Maker.RiseEngine.Core.Audio
{
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
            catch (Exception){}
        }

    }
}
