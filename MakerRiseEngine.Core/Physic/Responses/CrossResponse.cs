using Maker.RiseEngine.Core.MathExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Physic.Responses
{
    public class CrossResponse : ICollisionResponse
    {
        public CrossResponse(ICollision collision)
        {
            this.Destination = collision.Goal;
        }

        public RectangleF Destination { get; private set; }
    }
}
