using Maker.RiseEngine.Core.Ressources;
using Maker.RiseEngine.Core.Storage;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Maker.RiseEngine.Core.Audio
{
    public class SoundEffectColection
    {
        public List<SoundEffect> soundEffects;

        public SoundEffectColection(string pluginName, string name) {

            soundEffects = new List<SoundEffect>();

            System.IO.StreamReader sr = new System.IO.StreamReader("Plugins\\" + pluginName + "\\assets\\sounds_effects\\" + name + ".rise");
            string f = sr.ReadToEnd().ToDosLineEnd();
            sr.Close();

            f = f.Replace(System.Environment.NewLine, "");
            string[] Ls = f.Split(',');


            for (int i = 0; i < Ls.Length; i++)
            {

                soundEffects.Add(rise.ENGINE.RESSOUCES.SoundEffect(pluginName, Ls[i]));

            }

        }
    }
}
