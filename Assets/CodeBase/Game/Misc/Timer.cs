using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Game.Misc
{
    public class Timer
    {
        private bool _active;
        private float _time;
        private float _currentTime;
        private float _step;
        private Action _invokeComplite;

        private CancellationTokenSource _cts; 

        public bool Active => _active;

        public Timer() 
        { 
            _active = false;
        }

        public Timer Initialize(float time, float step = 0.1f, Action callback = null)
        {
            _time = time;
            _currentTime = _time;
            _step = step;
            _invokeComplite = callback;
            return this;
        }

        public void Play()
        {
            if (_currentTime == 0)
                return;
            _cts = new();
            _active = true;
            ExecuteAsync();
        }

        public void Pause()
            => _cts.Cancel();

        public void Stop()
        {
            _currentTime = 0;
            _invokeComplite = null;
            _cts.Cancel();
        }

        private async UniTask ExecuteAsync()
        {
            do
            {
                await UniTask.Delay(TimeSpan.FromSeconds(Mathf.Min(_step, _currentTime)),
                    delayTiming: PlayerLoopTiming.FixedUpdate, cancellationToken: _cts.Token);

                if (!_cts.IsCancellationRequested)
                    _currentTime -= _step;

            } while (_currentTime > 0f && !_cts.IsCancellationRequested);

            _active = false;

            if(_currentTime <= 0f && !_cts.IsCancellationRequested)
                _invokeComplite?.Invoke();
        }
    }
}