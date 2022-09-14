using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ImageTimer : MonoBehaviour
    {
        [SerializeField] private bool _autoStart = false;
        [SerializeField] private float _maxTime;
        [SerializeField] private bool _countDown; // Отчет на убывание, уменьшается индикатор
        private Image _img;
        private float _currentTime;
        private bool _started = false;
        private bool _loop = true;
        [HideInInspector] public bool Tick = false; // Идет отчет, зависит от _countDown

        private void Awake()
        {
            _img = GetComponent<Image>();
            SetCurrentTime();
            StopTimer();
        }
        private void Start()
        {
            if (_autoStart)
            {
                StartTimer();
            }
        }
        private void Update()
        {
            if (_started)
            {
                if (_countDown) // На убывание
                {
                    Tick = false;
                    _currentTime -= Time.deltaTime;

                    if (_currentTime <= 0)
                    {
                        Tick = true;
                        if (_loop)
                        {
                            _currentTime = _maxTime;
                        }
                        else
                        {
                            StopTimer(true);
                        }
                    }
                }
                else // На возрастание
                {
                    Tick = false;
                    _currentTime += Time.deltaTime;

                    if (_currentTime >= _maxTime)
                    {
                        Tick = true;
                        if (_loop)
                        {
                            _currentTime = 0;
                        }
                        else
                        {
                            StopTimer(true);
                        }
                    }
                }

                _img.fillAmount = _currentTime / _maxTime;
            }
        }

        public void SetMaxTime(int value)
        {
            _maxTime = value;
            if (_countDown) // Не нравится, но ХЗ как по другому
            {
                _currentTime = value;
            }
        }
        public void SetLoop(bool value)
        {
            _loop = value;
        }
        public void StartTimer()
        {
            _started = true;
        }
        public void StopTimer()
        {
            _started = false;
        }
        public void StopTimer(bool reset)
        {
            StopTimer();
            if (reset)
            {
                SetCurrentTime();
            }
        }

        public bool IsStarted()
        {
            return _started;
        }

        private void SetCurrentTime()
        {
            if (_countDown)
            {
                _currentTime = _maxTime;
            }
            else
            {
                _currentTime = 0;
            }
        }
        public float GetCurrentTime()
        {
            return _currentTime;
        }
    }
}