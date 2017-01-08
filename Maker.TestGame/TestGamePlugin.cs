using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.EngineDebug;
using Maker.RiseEngine.Core.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.TestGame
{
    public class TestGamePlugin : IPlugin
    {
        public string Name { get; } = "TestPlugin";

        public void Initialize<PluginType>(PluginLoader<PluginType> pluginLoader) where PluginType : IPlugin
        {

        }
    }
}
