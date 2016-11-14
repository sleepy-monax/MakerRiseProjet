using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Plugin
{
    interface IPlugin : IInitializable
    {

        string pluginName { get; }
        string pluginCreator { get;  }

    }
}
