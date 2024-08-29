using Game.Enemy;
using System;
using UnityEngine;
using Zenject;

namespace Game.Items
{
    public class ItemSpawner : IInitializable, IDisposable
    {
        private readonly EnemySpawner _enemySpawner;
        private readonly AmmoItemHandler.AmmoSettings _settings;
        private readonly AmmoItemHandler.Pool _itemPool;

        public ItemSpawner(EnemySpawner enemySpawner,
            AmmoItemHandler.AmmoSettings ammoSettings, AmmoItemHandler.Pool pool)
        {
            _enemySpawner = enemySpawner;
            _settings = ammoSettings;
            _itemPool = pool;
        }

        public void Initialize()
        {
            _enemySpawner.InvokeDeath += Spawn;
        }

        public void Dispose()
        {
            _enemySpawner.InvokeDeath -= Spawn;
        }

        public void Reset()
        {
            _itemPool.Reset();
        }

        private void Spawn(Vector2 position)
        {
            if (UnityEngine.Random.Range(0f, 1f) <= _settings.Chance)
                _itemPool.Spawn(position);
        }
    }
}