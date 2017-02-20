using Maker.RiseEngine.Core.MathExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Physic
{
    public interface IMovement
    {
        IEnumerable<IHit> Hits { get; }

        bool HasCollided { get; }

        RectangleF Origin { get; }

        RectangleF Goal { get; }

        RectangleF Destination { get; }
    }
}
