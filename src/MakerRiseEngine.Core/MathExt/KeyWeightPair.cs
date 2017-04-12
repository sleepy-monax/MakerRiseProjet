namespace Maker.RiseEngine.Core.MathExt
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
