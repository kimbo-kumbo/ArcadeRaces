using UnityEngine;
using System;
using UnityEngine.UI;

namespace RacePrototype
{
    public class Timer : MonoBehaviour
    {
        private DateTime _startTime = new();
        private DateTime _now = new();
        private TimeSpan _interval;
        [SerializeField]
        private Text _timerText;

        private void Start()
        {
            ResetTimer();
        }

        private void ResetTimer()
        {
            _startTime = DateTime.Now;
        }

        private void Update()
        {
            CalculateTimeElapsed();
            ConvertTimeElapsedToString();
            _timerText.text = ConvertTimeElapsedToString();
        }

        private string ConvertTimeElapsedToString()
        {
            return _interval.Minutes + ":" + _interval.Seconds + ":" + _interval.Milliseconds / 100;
        }
        public float ConvertTimeElapsedToFloat()
        {
            return _interval.Minutes * 60 + _interval.Seconds + ((float)(_interval.Milliseconds / 100)) / 10;
        }

        private void CalculateTimeElapsed()
        {
            _now = DateTime.Now;
            _interval = _now - _startTime;
        }
    }
}