using Game.Player;
using Game.StaticData;
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
        private PlayerHandler _player;

        public void TakeDamage(int damage)
        {
            _damageHandler.TakeDamage(damage);
        }

        [Inject]
        private void Construct(EnemyDamageHandler damageHandler,
            EnemyMoveHandler moveHandler, 
            PlayerHandler player)
        {
            _damageHandler = damageHandler;
            _player = player;
            _moveHandler = moveHandler;
        }

        private void Reinitialize(Vector2 spawn)
        {
            _damageHandler.Reset();
            _moveHandler.Reset(spawn);
        }

        private void Awake()
        {
            _damageHandler.InvokeHitPointChange += OnHitChange;
        }

        private void OnDestroy()
        {
            _damageHandler.InvokeHitPointChange -= OnHitChange;
        }

        private void OnHitChange(int currentHits)
        {
            Debug.Log(currentHits);
            if (currentHits <= 0)
                InvokeDeath?.Invoke(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.tag != TagsNames.Player)
                return;
            _player.TakeDamage(damage: 1);
        }

        public class Pool : MonoMemoryPool<Vector2, EnemyHandler>
        {
            protected override void Reinitialize(Vector2 spawn, EnemyHandler item)
            {
                base.Reinitialize(spawn, item);
                item.Reinitialize(spawn);
            }
        }
    }
}