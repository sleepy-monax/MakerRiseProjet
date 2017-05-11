using Maker.RiseEngine.Core.MathExt.Noise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Twiyol.Generator.EntitiesDistribution
{
    public class PerlinDistribution : IEntitiesDistributionRule
    {

        float NoiseStretch;
        PerlinNoise Noise;
        public PerlinDistribution(float noiseStretch, int seed) {

            NoiseStretch = noiseStretch;
            Noise = new PerlinNoise(seed);
        }

        public float GetValue(int x, int y)
        {

            return (float)Noise.Noise(x * NoiseStretch, y * NoiseStretch, 0);

        }
    }
}
