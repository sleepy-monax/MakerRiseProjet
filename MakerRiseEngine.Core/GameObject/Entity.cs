using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.GameObject
{
    public abstract class Entity : iGameObject
    {
        public string gameObjectName { get; set; }

        public string pluginName { get; set; }

        public abstract void OnGameObjectAdded();
    }
}
