using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.twiyol.Generator.EntitiesDistribution
{
    public class FlatDistribution : IEntitiesDistributionRule
    {
        float Value;

        public FlatDistribution(float value) {
            Value = value;
        }

        public float GetValue(int x, int y)
        {
            return Value;
        }
    }
}
