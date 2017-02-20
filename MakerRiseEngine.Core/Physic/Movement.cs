using Maker.RiseEngine.Core.MathExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Physic
{

    public class Movement : IMovement
    {
        public Movement()
        {
            this.Hits = new IHit[0];
        }

        public IEnumerable<IHit> Hits { get; set; }

        public bool HasCollided { get { return this.Hits.Any(); } }

        public RectangleF Origin { get; set; }

        public RectangleF Destination { get; set; }

        public RectangleF Goal { get; set; }
    }

}
