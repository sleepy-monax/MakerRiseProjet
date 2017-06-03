using Maker.RiseEngine.Core.Storage;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;

namespace Maker.RiseEngine.Core.Audio
{
    public class SoundEffectColection
    {
        public List<SoundEffect> SoundEffectList;

        public SoundEffectColection(string pluginName, string name) {

            SoundEffectList = new List<SoundEffect>();

            StreamReader sr = new StreamReader($"Plugins\\{pluginName}\\assets\\sounds_effects\\{name}.rise");
            string f = sr.ReadToEnd().ToDosLineEnd();
            sr.Close();

            f = f.Replace(Environment.NewLine, "");
            string[] Ls = f.Split(',');


            for (int i = 0; i < Ls.Length; i++)
            {

                SoundEffectList.Add(Rise.Engine.ressourceManager.GetSoundEffect(pluginName, Ls[i]));

            }

        }
    }
}
