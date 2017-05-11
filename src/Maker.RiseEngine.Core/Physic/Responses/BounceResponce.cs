using Maker.RiseEngine.Core.MathExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Physic.Responses
{
    public class BounceResponse : ICollisionResponse
    {
        public BounceResponse(ICollision collision)
        {
            var velocity = (collision.Goal.Center - collision.Origin.Center);
            var deflected = velocity * collision.Hit.Amount;

            if (Math.Abs(collision.Hit.Normal.X) > 0.00001f)
            {
                deflected.X *= -1;
            }

            if (Math.Abs(collision.Hit.Normal.Y) > 0.00001f)
            {
                deflected.Y *= -1;
            }

            this.Destination = new RectangleF(collision.Hit.Position + deflected, collision.Goal.Size);
        }

        public RectangleF Destination { get; private set; }
    }
}
