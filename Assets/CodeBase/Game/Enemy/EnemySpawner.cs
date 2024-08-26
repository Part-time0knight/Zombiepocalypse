using Cysharp.Threading.Tasks;
using Game.Misc;
using Installers;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class EnemySpawner
    {
        private readonly Timer _timer;
        private readonly EnemyHandler.Pool _pool;
        private readonly List<EnemyHandler> _enemies;
        private readonly Settings _settings;
        private readonly List<EnemyHandler.EnemyPreset> _enemyPresets;

        private bool _breakTimer;
        private Vector2 _spawnPosition;
        private float _delay;

        public EnemySpawner(EnemyHandler.Pool pool, 
            Settings settings,
            List<EnemyHandler.EnemyPreset> enemyPresets)
        {
            _pool = pool;
            _settings = settings;
            _timer = new Timer();
            _enemies = new();
            _enemyPresets = enemyPresets;
        }

        public void BeginSpawn()
        {
            _breakTimer = false;
            Repeater();
        }

        public void StopSpawn()
        {
            _breakTimer = true;
            foreach (var enemy in _enemies)
            {
                enemy.InvokeDeath -= OnDeath;
                _pool.Despawn(enemy);
            }
            _enemies.Clear();
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
                    var enemy = _pool.Spawn(_spawnPosition, _enemyPresets[_settings.CurrentEnemyIndex]);
                    enemy.InvokeDeath += OnDeath;
                    _enemies.Add(enemy);
                }
            } while (!_breakTimer);
        }

        private void OnDeath(EnemyHandler enemy)
        {
            enemy.InvokeDeath -= OnDeath;
            _enemies.Remove(enemy);
            _pool.Despawn(enemy);
        }

        private void CalculatePosition()
        {
            var x = UnityEngine.Random.Range(0, 2) == 0 ?
                _settings.HorizontalPoints.x : _settings.HorizontalPoints.y;

            _spawnPosition = new(x, _settings.Yposition);

            _delay = UnityEngine.Random.Range(_settings.SpawnDelay.x, _settings.SpawnDelay.y);

            _settings.CurrentEnemyIndex = UnityEngine.Random.Range(0, _enemyPresets.Count);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Vector2 HorizontalPoints { get; private set; }
            [field: SerializeField] public float Yposition { get; private set; }
            [field: SerializeField] public Vector2 SpawnDelay { get; private set; }
            [field: SerializeField] public Vector2 DisposePoint { get; private set; }

            public int CurrentEnemyIndex { get; set; }
        }
    }
}