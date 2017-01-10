using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Content
{
    public struct ContentEngine
    {

        public static ContentManager Content;

        public static Dictionary<string, Texture2D> ColectionTexture2D = new Dictionary<string, Texture2D>();
        public static Dictionary<string, SoundEffect> ColectionSoundEffect = new Dictionary<string, SoundEffect>();
        public static Dictionary<string, Song> ColectionSong = new Dictionary<string, Song>();
        public static Dictionary<string, SpriteFont> ColectionFont = new Dictionary<string, SpriteFont>();


        public static Texture2D Texture2D(string PluginName, string contentname)
        {

            if (ColectionTexture2D.ContainsKey(contentname))
            { return ColectionTexture2D[contentname]; }
            else
            {
                EngineDebug.DebugLogs.WriteLog("Load <Texture2D>" + PluginName + "." + contentname, EngineDebug.LogType.Info, "ContentEngine");
                ColectionTexture2D.Add(contentname, Content.Load<Texture2D>(PluginName + "/Textures2D/" + contentname));
                return ColectionTexture2D[contentname];
            }

        }

        public static Texture2D GetDefaultTexture2D()
        {

            Bitmap bitmap = new Bitmap(2, 2);
            bitmap.SetPixel(0, 0, Color.Fuchsia);
            bitmap.SetPixel(1, 1, Color.Fuchsia);
            bitmap.SetPixel(1, 0, Color.Black);
            bitmap.SetPixel(0, 1, Color.Black);

            return Rendering.BitmapHelper.BitmapToTexture2D(Engine.GraphicsDevice, bitmap);

        }

        public static SoundEffect SoundEffect(string PluginName, string contentname)
        {

            if (ColectionSoundEffect.ContainsKey(contentname))
            { return ColectionSoundEffect[contentname]; }
            else
            {
                EngineDebug.DebugLogs.WriteLog("Load <SoundEffect>" + PluginName + "." + contentname, EngineDebug.LogType.Info, "ContentEngine");
                ColectionSoundEffect.Add(contentname, Content.Load<SoundEffect>(PluginName + "/SoundsEffects/" + contentname));
                return ColectionSoundEffect[contentname];
            }

        }

        public static Song Song(string PluginName, string contentname)
        {

            if (ColectionSong.ContainsKey(contentname))
            { return ColectionSong[contentname]; }
            else
            {
                EngineDebug.DebugLogs.WriteLog("Load <Song>" + PluginName + "." + contentname, EngineDebug.LogType.Info, "ContentEngine");
                ColectionSong.Add(contentname, Content.Load<Song>(PluginName + "/Songs/" + contentname));
                return ColectionSong[contentname];
            }

        }

        public static SpriteFont SpriteFont(string PluginName, string contentname)
        {

            if (ColectionFont.ContainsKey(contentname))
            { return ColectionFont[contentname]; }
            else
            {
                EngineDebug.DebugLogs.WriteLog("Load <SpriteFont>" + PluginName + "." + contentname, EngineDebug.LogType.Info, "ContentEngine");
                ColectionFont.Add(contentname, Content.Load<SpriteFont>(PluginName + "/Fonts/" + contentname));
                return ColectionFont[contentname];
            }

        }

        public static void ReloadContent()
        {

            EngineDebug.DebugLogs.WriteLog("Reloading...", EngineDebug.LogType.Info, "ContentEngine");
            ColectionTexture2D.Clear();
            ColectionSoundEffect.Clear();
            ColectionSong.Clear();
            ColectionFont.Clear();

        }


    }
}
