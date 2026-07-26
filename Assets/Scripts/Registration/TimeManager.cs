using System;

namespace RegistrationNameSpace
{
    public class TimeManager
    {
        private double _timeOffset;
        private double _targetTime;
        private double _elapsedTime = 0f;

        public void Initialize(double timeOffset, double targetTime)
        {
            _timeOffset = timeOffset;
            _targetTime = targetTime;
            _elapsedTime = 0f;
        }
        
        public void UpdateTime(double deltaTime)
        {
            _elapsedTime += deltaTime;
        }
        
        public bool IsPastTime()
        {
            return (int)Math.Round(GetRemainingTime()) <= 0;
        }

        public double GetPastTime()
        {
            return _elapsedTime - _targetTime;
        }

        public double GetCurrentTime()
        {
            return _timeOffset + _elapsedTime;
        }

        public double GetRemainingTime()
        {
            return _targetTime - _elapsedTime;
        }
    }
}