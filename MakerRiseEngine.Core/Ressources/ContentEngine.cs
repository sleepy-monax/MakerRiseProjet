using Maker.RiseEngine.Core.Rendering;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

using System.Collections.Generic;
using System.Drawing;

namespace Maker.RiseEngine.Core.Ressources
{
    public class RessourcesManager
    {

        private ContentManager RESSOURCES;

        private Dictionary<string, Texture2D> IMAGES = new Dictionary<string, Texture2D>();
        private Dictionary<string, SoundEffect> SOUNDEFFECTS = new Dictionary<string, SoundEffect>();
        private Dictionary<string, Song> SONGS = new Dictionary<string, Song>();
        private Dictionary<string, SpriteFont> FONTS = new Dictionary<string, SpriteFont>();

        public RessourcesManager(ContentManager content) {

            RESSOURCES = content;

        }

        public Texture2D Texture2D(string PluginName, string contentname)
        {

            if (IMAGES.ContainsKey(contentname))
            {
                return IMAGES[contentname];
            }
            else
            {
                EngineDebug.DebugLogs.WriteLog("Load <Texture2D>" + PluginName + "." + contentname, EngineDebug.LogType.Info, "Ressources Manager");
                IMAGES.Add(contentname, RESSOURCES.Load<Texture2D>(PluginName + "/assets/images/" + contentname));
                return IMAGES[contentname];
            }

        }

        public SoundEffect SoundEffect(string PluginName, string contentname)
        {

            if (SOUNDEFFECTS.ContainsKey(contentname))
            {
                return SOUNDEFFECTS[contentname];
            }
            else
            {
                EngineDebug.DebugLogs.WriteLog("Load <SoundEffect>" + PluginName + "." + contentname, EngineDebug.LogType.Info, "Ressources Manager");
                SOUNDEFFECTS.Add(contentname, RESSOURCES.Load<SoundEffect>(PluginName + "/assets/sounds_effects/" + contentname));
                return SOUNDEFFECTS[contentname];
            }

        }

        public Song Song(string PluginName, string contentname)
        {

            if (SONGS.ContainsKey(contentname))
            {
                return SONGS[contentname];
            }
            else
            {
                EngineDebug.DebugLogs.WriteLog("Load <Song>" + PluginName + "." + contentname, EngineDebug.LogType.Info, "Ressources Manager");
                SONGS.Add(contentname, RESSOURCES.Load<Song>(PluginName + "/assets/songs/" + contentname));
                return SONGS[contentname];
            }

        }

        public SpriteFont SpriteFont(string PluginName, string contentname)
        {

            if (FONTS.ContainsKey(contentname))
            { return FONTS[contentname]; }
            else
            {
                EngineDebug.DebugLogs.WriteLog("Load <SpriteFont>" + PluginName + "." + contentname, EngineDebug.LogType.Info, "Ressources Manager");
                FONTS.Add(contentname, RESSOURCES.Load<SpriteFont>(PluginName + "/assets/fonts/" + contentname));
                return FONTS[contentname];
            }

        }

    }
}
