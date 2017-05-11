using Maker.RiseEngine.Core.MathExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Physic.Responses
{
    public interface ICollisionResponse
    {
        /// <summary>
        /// Gets the new destination of the box after the collision.
        /// </summary>
        /// <value>The destination.</value>
        RectangleF Destination { get; }
    }
}
