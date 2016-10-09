using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace RiseEngine.Core.Audio
{
    public class SoundEffectColection
    {

        public List<SoundEffect> SE;

        public SoundEffectColection() {

            SE = new List<SoundEffect>();

        }

       

    }

    public static class SoundEffectParser {

        public static SoundEffectColection Parse(string _PluginName, string _Name) {

            

            System.IO.StreamReader sr = new System.IO.StreamReader("Data\\" + _PluginName + "\\SoundsEffects\\" + _Name + ".rise");
            string f = sr.ReadToEnd();
            sr.Close();

            f = f.Replace(System.Environment.NewLine, "");
            string[] Ls = f.Split(',');

            SoundEffectColection SE = new SoundEffectColection();

            for (int i = 0; i < Ls.Length; i++)
            {

                SE.SE.Add(ContentEngine.SoundEffect(_PluginName, Ls[i]));

            }

            return SE;
        }

    }
}
