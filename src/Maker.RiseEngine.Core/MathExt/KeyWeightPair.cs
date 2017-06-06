namespace Maker.RiseEngine.MathExt
{
    public class KeyWeightPair<T>
    {
        public T Value { get; set; }
        public double Weight { get; set; }

        public KeyWeightPair(T value, double weight) {
            Value = value;
            Weight = weight;
        }
    }
}
