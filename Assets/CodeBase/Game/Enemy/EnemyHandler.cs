using Game.Player;
using Game.StaticData;
using Presentation.View;
using System;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class EnemyHandler : MonoBehaviour
    {
        public event Action<EnemyHandler> InvokeDeath;

        private EnemyDamageHandler _damageHandler;
        private EnemyMoveHandler _moveHandler;
        private EnemyAnimation _animation;
        private EnemyWindowFsm _windowFsm;
        private PlayerHandler _player;

        public void TakeDamage(int damage)
        {
            _damageHandler.TakeDamage(damage);
        }

        [Inject]
        private void Construct(EnemyDamageHandler damageHandler,
            EnemyMoveHandler moveHandler,
            EnemyAnimation animation,
            EnemyWindowFsm windowFsm,
            PlayerHandler player)
        {
            _damageHandler = damageHandler;
            _player = player;
            _moveHandler = moveHandler;
            _animation = animation;
            _windowFsm = windowFsm;
        }

        private void Reinitialize(Vector2 spawn, EnemyPreset preset)
        {
            _moveHandler.Reset(preset, spawn);
            _damageHandler.Reset(preset);
            _animation.Reset(preset);
            _windowFsm.OpenWindow(typeof(EnemyHealthView), inHistory: true);
        }

        private void Despawn()
        {
            _animation.Disable();
            _moveHandler.Disable();
        }

        private void Awake()
        {
            _damageHandler.InvokeHitPointChange += OnHitChange;
        }

        private void OnDestroy()
        {
            _damageHandler.InvokeHitPointChange -= OnHitChange;
        }

        private void OnHitChange()
        {
            if (_damageHandler.CurrentHits <= 0)
            {
                InvokeDeath?.Invoke(this);
                _windowFsm.CloseWindow();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.tag != TagsNames.Player)
                return;
            _player.TakeDamage(damage: 1);
        }

        public class Pool : MonoMemoryPool<Vector2, EnemyPreset, EnemyHandler>
        {
            protected Transform _buffer;

            [Inject]
            private void Construct(EnemyBuffer buffer)
            {
                _buffer = buffer.transform;
            }

            protected override void OnCreated(EnemyHandler item)
            {
                item.transform.SetParent(_buffer);
                item.Despawn();
                base.OnCreated(item);
            }

            protected override void OnDespawned(EnemyHandler item)
            {
                base.OnDespawned(item);
                item.Despawn();
            }

            protected override void Reinitialize(Vector2 spawn, EnemyPreset preset, EnemyHandler item)
            {
                base.Reinitialize(spawn, preset, item);
                item.Reinitialize(spawn, preset);
            }
        }

        [Serializable]
        public class EnemyPreset
        {
            [field: SerializeField] public AnimationClip Clip { get; private set; }
            [field: SerializeField] public int Hits { get; private set; }
            [field: SerializeField] public float Speed { get; private set; }
        }
    }
}