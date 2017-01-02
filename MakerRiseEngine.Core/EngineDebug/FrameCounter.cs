using System.Collections.Generic;
using System.Linq;


namespace Maker.RiseEngine.Core.EngineDebug
{
    public static class FrameCounter
    {
        public static long TotalFrames { get; private set; }
        public static float TotalSeconds { get; private set; }
        public static float AverageFramesPerSecond { get; private set; }
        public static float AverageFramesTime { get; set; }
        public static float CurrentFramesPerSecond { get; private set; }

        public const int MAXIMUM_SAMPLES = 300;

        public static Queue<float> _sampleBuffer = new Queue<float>();
        public static Queue<float> _sampleFrameTimeBuffer = new Queue<float>();

        public static void Update(float deltaTime)
        {
            CurrentFramesPerSecond = 1.0f / deltaTime;
            _sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (_sampleBuffer.Count > MAXIMUM_SAMPLES)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }



            TotalFrames++;
            TotalSeconds += deltaTime;
        }
    }
}
