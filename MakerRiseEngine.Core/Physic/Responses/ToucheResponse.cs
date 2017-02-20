using Maker.RiseEngine.Core.MathExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Physic.Responses
{
    public class TouchResponse : ICollisionResponse
    {
        public TouchResponse(ICollision collision)
        {
            this.Destination = new RectangleF(collision.Hit.Position, collision.Goal.Size);
        }

        public RectangleF Destination { get; private set; }
    }
}
