using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.GameMath
{
    public class KeyWeightPair<T>
    {

        public T Value { get; set; }
        public double Weight { get; set; }

        public KeyWeightPair(T _Value, double _Weight) {
            Value = _Value;
            Weight = _Weight;
        }

    }
}
