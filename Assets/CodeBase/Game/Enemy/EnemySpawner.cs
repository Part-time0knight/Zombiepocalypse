using Cysharp.Threading.Tasks;
using Game.Misc;
using System;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class EnemySpawner : IInitializable
    {
        private readonly Timer _timer;
        private readonly EnemyHandler.Pool _pool;
        private readonly Settings _settings;

        private bool _breakTimer;
        private Vector2 _spawnPosition;
        private float _delay;

        public EnemySpawner(EnemyHandler.Pool pool, Settings settings)
        {
            _pool = pool;
            _settings = settings;
            _timer = new Timer();
        }

        public void Initialize()
        {
            BeginSpawn();
        }

        public void BeginSpawn()
        {
            _breakTimer = false;
            Repeater();
        }

        public void StopSpawn()
        {
            _breakTimer = true;
        }

        private async UniTask Repeater()
        {
            do
            {
                await UniTask.WaitWhile(() => _timer.Active);
                if (!_breakTimer)
                {
                    CalculatePosition();
                    _timer.Initialize(_delay).Play();
                    var enemy = _pool.Spawn(_spawnPosition);
                    enemy.InvokeDeath += OnDeath;
                }
            } while (!_breakTimer);
        }

        private void OnDeath(EnemyHandler enemy)
        {
            enemy.InvokeDeath -= OnDeath;
            _pool.Despawn(enemy);
        }

        private void CalculatePosition()
        {
            var x = UnityEngine.Random.Range(0, 2) == 0 ?
                _settings.HorizontalPoints.x : _settings.HorizontalPoints.y;

            _spawnPosition = new(x, _settings.Yposition);

            _delay = UnityEngine.Random.Range(_settings.SpawnDelay.x, _settings.SpawnDelay.y);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Vector2 HorizontalPoints { get; private set; }
            [field: SerializeField] public float Yposition { get; private set; }
            [field: SerializeField] public Vector2 SpawnDelay { get; private set; }
        }
    }
}