using Maker.RiseEngine.Core.Content;
using Maker.RiseEngine.Core.Storage;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Maker.RiseEngine.Core.Audio
{
    public class SoundEffectColection
    {
        public List<SoundEffect> soundEffects;

        public SoundEffectColection() {

            soundEffects = new List<SoundEffect>();

        }
    }

    public static class SoundEffectParser {

        public static SoundEffectColection Parse(string _PluginName, string _Name) {

            System.IO.StreamReader sr = new System.IO.StreamReader("Data\\" + _PluginName + "\\SoundsEffects\\" + _Name + ".rise");
            string f = sr.ReadToEnd().ToDosLineEnd();
            sr.Close();

            f = f.Replace(System.Environment.NewLine, "");
            string[] Ls = f.Split(',');

            SoundEffectColection soundEffectColection = new SoundEffectColection();

            for (int i = 0; i < Ls.Length; i++)
            {

                soundEffectColection.soundEffects.Add(ContentEngine.SoundEffect(_PluginName, Ls[i]));

            }

            return soundEffectColection;
        }

    }
}
