using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maker.RiseEngine.Core.Plugin
{
    public interface IPlugin
    {
        string Name { get; }
        void Initialize();
        void OnWorldGeneration(World.WorldScene world);
    }
}
