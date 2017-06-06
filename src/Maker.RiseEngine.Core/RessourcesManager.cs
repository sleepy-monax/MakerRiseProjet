using Maker.RiseEngine.EngineDebug;
using Maker.RiseEngine.Rendering.SpriteSheets;
using Maker.RiseEngine.Ressources;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

using System.Collections.Generic;
using System.IO;

namespace Maker.RiseEngine
{
    public class RessourcesManager
    {
        //GameModules.
        private ContentManager GameContentManager;
        private GameEngine Engine;

        // Loaded ressources.
        private Dictionary<string, Texture2D>   LoadedTextures     = new Dictionary<string, Texture2D>();
        private Dictionary<string, SoundEffect> LoadedSoundEffects = new Dictionary<string, SoundEffect>();
        private Dictionary<string, Song>        LoadedSongs        = new Dictionary<string, Song>();
        private Dictionary<string, SpriteFont>  LoadedSpriteFonts  = new Dictionary<string, SpriteFont>();
        private Dictionary<string, SpriteSheet> LoadedSpriteSheets = new Dictionary<string, SpriteSheet>();

        public RessourcesManager(GameEngine gameEngine, ContentManager gameContentManager) {
            GameContentManager = gameContentManager;
            Engine = gameEngine;
        }

        /// <summary>
        /// Get a texture from the ressource manager.
        /// </summary>
        /// <param name="pluginName">Name of the plugin to get the ressources from.</param>
        /// <param name="ressourceName">Name of the ressource.</param>
        /// <returns></returns>
        public Texture2D GetTexture2D(string pluginName, string ressourceName)
        {

            if (LoadedTextures.ContainsKey(ressourceName)) return LoadedTextures[ressourceName];
            else
            {
                Debug.WriteLog("Load <Texture2D>" + pluginName + "." + ressourceName, LogType.Info, "Ressources Manager");
                LoadedTextures.Add(ressourceName, Texture2D.FromStream(Engine.GraphicsDevice, new FileStream($"Plugins/{pluginName}/assets/Textures/{ressourceName}.png", FileMode.Open)));
                return LoadedTextures[ressourceName];
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
                Debug.WriteLog($"Load <{nameof(SoundEffect)}>{pluginName}.{ressourceName}", LogType.Info, nameof(RessourcesManager));
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
            if (LoadedSongs.ContainsKey(ressourceName)) return LoadedSongs[ressourceName];
            else
            {
                Debug.WriteLog($"Load <{nameof(Song)}>{pluginName}.{ressourceName}", LogType.Info, nameof(RessourcesManager));
                LoadedSongs.Add(ressourceName, GameContentManager.Load<Song>($"{pluginName}/assets/songs/{ressourceName}"));
                return LoadedSongs[ressourceName];
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
            if (LoadedSpriteFonts.ContainsKey(ressourceName)) return LoadedSpriteFonts[ressourceName];
            else
            {
                Debug.WriteLog($"Load <{nameof(SpriteFont)}>{pluginName}.{ressourceName}", LogType.Info, nameof(RessourcesManager));
                LoadedSpriteFonts.Add(ressourceName, GameContentManager.Load<SpriteFont>($"{pluginName}/assets/fonts/{ressourceName}"));
                return LoadedSpriteFonts[ressourceName];
            }
        }

        /// <summary>
        /// Get a Spritesheet from the ressource manager.
        /// </summary>
        /// <param name="pluginName">Name of the plugin to get the ressources from.</param>
        /// <param name="ressourceName">Name of the ressource.</param>
        /// <returns></returns>
        public SpriteSheet GetSpriteSheet(string pluginName, string ressourceName)
        {
            if (!LoadedSpriteSheets.ContainsKey(ressourceName))
            {
                Debug.WriteLog($"Load <{nameof(SpriteSheet)}>{pluginName}.{ressourceName}", LogType.Info, nameof(RessourcesManager));
                SpriteSheet spriteSheet = new SpriteSheet(Engine, pluginName, new DataFile($"Plugins/{pluginName}/assets/SpriteSheets/{ressourceName}.risedata"));
                LoadedSpriteSheets.Add(ressourceName, spriteSheet);
            }
            return LoadedSpriteSheets[ressourceName];
        }
    }
}
