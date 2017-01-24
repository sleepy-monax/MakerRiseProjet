using Microsoft.Xna.Framework.Input;
using System;

namespace Maker.RiseEngine.Core.Config
{
    [Serializable]
    public class EngineConfig
    {
        //Engine.
        public int Engine_SplashTime { get; set; } = 2000;

        // Sound Config.
        public float Sound_Master_Level { get; set; } = 1f;
        public float Sound_Song_Level { get; set; } = 1f;
        public float Sound_Effect_Level { get; set; }  = 1f;

        // Debug.
        public bool Debug_EnableLogs { get; set; } = true;
        public bool Debug_FrameCounter { get; set; } = false;
        public bool Debug_GuiFrame { get; set; } = false;
        public bool Debug_WorldOverDraw { get; set; } = false;
        public bool Debug_WorldFocusLocation { get; set; } = false;
        public bool Debug_DebugWaterMark { get; set; } = false;

        // GFX
        public int GFX_ViewDistance { get; set; } = 16;
        public bool GFX_FullScreen { get; set; } = false;

        // Input.
        public Keys Input_ShowChat { get; set; } = Keys.T;
        public Keys Input_ShowMenu { get; set; } = Keys.Escape;

        public Keys Input_MoveUp { get; set; } = Keys.Z;
        public Keys Input_MoveLeft { get; set; } = Keys.Q;
        public Keys Input_MoveDown { get; set; } = Keys.S;
        public Keys Input_MoveRight { get; set; } = Keys.D;

        public Keys Input_Attack { get; set; } = Keys.A;

        public Keys Input_Take_Screenshot { get; set; } = Keys.F2;
    }
}
