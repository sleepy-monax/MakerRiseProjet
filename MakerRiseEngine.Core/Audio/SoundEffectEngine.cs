using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Maker.RiseEngine.Core.Audio
{
    public static class SoundEffectEngine
    {

        static List<SoundEffectInstance> SEI;
        static Random rnd;

        static SoundEffectEngine()
        {

            SEI = new List<SoundEffectInstance>();
            rnd = new Random();

        }

        public static void PlaySoundEffects(SoundEffectColection SE)
        {

            int i = rnd.Next(SE.SE.Count);
            SoundEffect se = SE.SE[i];

            SoundEffectInstance seI = se.CreateInstance();
            SEI.Add(seI);
            seI.Volume = (Engine.engineConfig.Sound_Master_Level * Engine.engineConfig.Sound_Effect_Level);
            seI.Play();

        }

        public static void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {
            try
            {
                foreach (SoundEffectInstance i in SEI)
                {

                    if (i.State == SoundState.Stopped)
                    {
                        SEI.Remove(i);
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
