using System;
using System.Collections.Generic;
using System.Linq;

namespace Maker.RiseEngine.MathExt
{
    public class RandomHelper
    {
        public static string RandomString(int length, string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        public static T GetRandomValueByWeight<T>(KeyWeightPair<T>[] keyWeightPairArray, Random _Rnd)
        {
            double RndNumber = _Rnd.NextDouble();

            foreach (var item in keyWeightPairArray)
            {
                if (RndNumber < item.Weight)
                    return item.Value;
                RndNumber -= item.Weight;
            }
            throw new InvalidOperationException("The proportions in the collection do not add up to 1.");
        }
    }
}
