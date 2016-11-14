using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.GameObject
{
    interface IGameObject
    {

        string gameObjectName { get; set; }
        string gameObjectParentPluginName { get; set; }

    }
}
