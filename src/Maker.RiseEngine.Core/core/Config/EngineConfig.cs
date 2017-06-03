using Microsoft.Xna.Framework.Input;
using System;

namespace Maker.RiseEngine.Core.Config
{
    [Serializable]
    public class EngineUserConfig
    {
        //Engine.
        public int EngineSplashScreenTime  { get; set; } = 0;
        public string EngineSelectedProfil { get; set; } = "TWIYOL";

        // Sound Config.
        public float SoundMasterLevel { get; set; } = 1f;
        public float SoundSongLevel   { get; set; } = 1f;
        public float SoundEffectLevel { get; set; }  = 1f;

        // Debug.
        public bool DebugEnableLogs          { get; set; } = true;
        public bool DebugShowFrameCounter    { get; set; } = false;
        public bool DebugShowGuiFrame        { get; set; } = false;
        public bool DebugShowDebugWaterMark  { get; set; } = false;
        public bool DebugShowLoadedSceneList { get; set; } = false;
        public bool DebugShowErrorMessages   { get; set; } = false;

        // GFX
        public int GraphicsViewDistance      { get; set; } = 16;
        public bool GraphicsEnableFullscreen { get; set; } = false;

        // Input.
        public Keys InputShowChat      { get; set; } = Keys.T;
        public Keys InputShowMainMenu  { get; set; } = Keys.Escape;
        public Keys InputShowInventory { get; set; } = Keys.I;
        public Keys InputMoveUp        { get; set; } = Keys.Z;
        public Keys InputMoveLeft      { get; set; } = Keys.Q;
        public Keys InputMoveDown      { get; set; } = Keys.S;
        public Keys InputMoveRight     { get; set; } = Keys.D;
        public Keys InputAttack        { get; set; } = Keys.A;
        public Keys InputScreenshot    { get; set; } = Keys.F2;
    }
}
