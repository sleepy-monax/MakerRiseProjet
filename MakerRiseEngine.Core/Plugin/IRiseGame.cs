﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Plugin
{
    public interface IRiseGame
    {

        void OnEngineInitialization();
        void OnLoadContent();
        void OnDraw();
        void OnUpdate();

    }
}
