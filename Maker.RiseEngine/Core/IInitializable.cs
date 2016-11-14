using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core
{
    interface IInitializable
    {

        void Initialize(MakerRiseGame game);
        void Deinitialize(MakerRiseGame game);

    }
}
