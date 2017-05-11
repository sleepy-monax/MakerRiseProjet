using Maker.RiseEngine.Core.EngineDebug;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

using System.Collections.Generic;
using System.IO;

namespace Maker.RiseEngine.Core
{
    public class RessourcesManager
    {
        //GameModules.
        private ContentManager GameContentManager;
        private engine GameEngine;

        // Loaded ressources.
        private Dictionary<string, Texture2D>   LoadedImages        = new Dictionary<string, Texture2D>();
        private Dictionary<string, SoundEffect> LoadedSoundEffects  = new Dictionary<string, SoundEffect>();
        private Dictionary<string, Song>        LoadedSong          = new Dictionary<string, Song>();
        private Dictionary<string, SpriteFont>  LoadedFonts         = new Dictionary<string, SpriteFont>();

        public RessourcesManager(engine gameEngine, ContentManager gameContentManager) {
            GameContentManager = gameContentManager;
            GameEngine = gameEngine;
        }

        /// <summary>
        /// Get a texture from the ressource manager.
        /// </summary>
        /// <param name="pluginName">Name of the plugin to get the ressources from.</param>
        /// <param name="ressourceName">Name of the ressource.</param>
        /// <returns></returns>
        public Texture2D GetTexture2D(string pluginName, string ressourceName)
        {

            if (LoadedImages.ContainsKey(ressourceName)) return LoadedImages[ressourceName];
            else
            {
                DebugLogs.WriteLog("Load <Texture2D>" + pluginName + "." + ressourceName, LogType.Info, "Ressources Manager");
                LoadedImages.Add(ressourceName, Texture2D.FromStream(GameEngine.GraphicsDevice, new FileStream($"Plugins/{pluginName}/assets/images/{ressourceName}.png", FileMode.Open)));
                return LoadedImages[ressourceName];
            }

        }

        /// <summary>
        /// Get a SoundEffect from the ressources manager.
        /// </summary>
        /// <param name="pluginName">Name of the plugin to get ressources from.</param>
        /// <param name="ressourceName">Name of the ressource.</param>
        /// <returns></returns>
        public SoundEffect GetSoundEffect(string pluginName, string ressourceName)
        {
            if (LoadedSoundEffects.ContainsKey(ressourceName)) return LoadedSoundEffects[ressourceName];
            else
            {
                DebugLogs.WriteLog($"Load <{nameof(SoundEffect)}>{pluginName}.{ressourceName}", LogType.Info, nameof(RessourcesManager));
                LoadedSoundEffects.Add(ressourceName, GameContentManager.Load<SoundEffect>($"{pluginName}/assets/sounds_effects/{ressourceName}"));
                return LoadedSoundEffects[ressourceName];
            }
        }

        /// <summary>
        /// Get a Song from the ressource manager.
        /// </summary>
        /// <param name="pluginName">Name of the plugin to get the ressources from.</param>
        /// <param name="ressourceName">Name of the ressource.</param>
        /// <returns></returns>
        public Song GetSong(string pluginName, string ressourceName)
        {
            if (LoadedSong.ContainsKey(ressourceName)) return LoadedSong[ressourceName];
            else
            {
                DebugLogs.WriteLog($"Load <{nameof(Song)}>{pluginName}.{ressourceName}", LogType.Info, nameof(RessourcesManager));
                LoadedSong.Add(ressourceName, GameContentManager.Load<Song>($"{pluginName}/assets/songs/{ressourceName}"));
                return LoadedSong[ressourceName];
            }
        }

        /// <summary>
        /// Get a SpriteFont from the ressource manager.
        /// </summary>
        /// <param name="pluginName">Name of the plugin to get the ressources from.</param>
        /// <param name="ressourceName">Name of the ressource.</param>
        /// <returns></returns>
        public SpriteFont GetSpriteFont(string pluginName, string ressourceName)
        {
            if (LoadedFonts.ContainsKey(ressourceName)) return LoadedFonts[ressourceName];
            else
            {
                DebugLogs.WriteLog($"Load <{nameof(SpriteFont)}>{pluginName}.{ressourceName}", LogType.Info, nameof(RessourcesManager));
                LoadedFonts.Add(ressourceName, GameContentManager.Load<SpriteFont>($"{pluginName}/assets/fonts/{ressourceName}"));
                return LoadedFonts[ressourceName];
            }
        }
    }
}
