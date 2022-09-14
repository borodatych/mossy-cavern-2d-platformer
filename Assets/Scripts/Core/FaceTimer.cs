using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class FaceTimer : Singleton<FaceTimer>
    {
        private IEnumerator _coroutine;

        [SerializeField] private int _timer = 0;
        [SerializeField] private float _timerSpeed = 1.0f;
        [SerializeField] private bool _autoStart = false;

        public int Timer
        {
            get => _timer;
            set => _timer = value;
        }

        #region From_Object
        private void Awake()
        {
            if (_autoStart)
            {
                _coroutine = Time();
            }
        }
        private IEnumerator Time()
        {
            while (true)
            {
                TimeCount();
                yield return new WaitForSeconds(1 * _timerSpeed);
            }
        }
        public void TimeCount()
        {
            _timer += 1;;
        }
        public void StartTimer()
        {
            StartCoroutine(_coroutine);
        }
        public void StopTimer()
        {
            StopCoroutine(_coroutine);
        }
        public void StopTimer(bool reset)
        {
            StopTimer();
            if (reset)
            {
                ResetTimer();
            }
        }
        public void ResetTimer()
        {
            _timer = 0;
        }
        public void RestartTimer()
        {
            StopTimer();
            ResetTimer();
            StartTimer();
        }
        public int GetTimer()
        {
            return _timer;
        }
        #endregion

        #region For_Paused
        public void Freeze(Action callback)
        {
            StartCoroutine(CountDown(callback));
        }
        public void Freeze(float time, Action callback)
        {
            StartCoroutine(CountDown(time, callback));
        }
        public IEnumerator CountDown(Action callback)
        {
            yield return new WaitForSeconds(_timer * _timerSpeed);
            callback();
        }
        public IEnumerator CountDown(float time, Action callback)
        {
            yield return new WaitForSeconds(time * _timerSpeed);
            callback();
        }
        #endregion
    }
}