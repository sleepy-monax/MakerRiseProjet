using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Maker.RiseEngine.Core.Audio
{
    public static class SoundEffectEngine
    {

        static List<SoundEffectInstance> soundEffectInstance;
        static Random random;

        static SoundEffectEngine()
        {

            soundEffectInstance = new List<SoundEffectInstance>();
            random = new Random();

        }

        public static void PlaySoundEffect(SoundEffectColection soundEffectColection)
        {

            int i = random.Next(soundEffectColection.soundEffects.Count);
            SoundEffect soundEffect = soundEffectColection.soundEffects[i];

            SoundEffectInstance newSoundEffectInstance = soundEffect.CreateInstance();
            soundEffectInstance.Add(newSoundEffectInstance);
            newSoundEffectInstance.Volume = (Engine.engineConfig.Sound_Master_Level * Engine.engineConfig.Sound_Effect_Level);
            newSoundEffectInstance.Play();

        }

        public static void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
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
                    else
                    {

                        i.Volume = (Engine.engineConfig.Sound_Master_Level * Engine.engineConfig.Sound_Effect_Level);

                    }
                }
            }
            catch (Exception){}
        }

    }
}
