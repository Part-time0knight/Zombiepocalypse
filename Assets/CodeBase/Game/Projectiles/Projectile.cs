using Game.Misc;
using System;
using UnityEngine;
using Zenject;

namespace Game.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        public event Action<Projectile, GameObject> InvokeHit;
        public event Action<Projectile> InvokeRemove;

        protected ProjectileMoveHadler _moveHandler;
        protected Timer _timer = new();
        protected Vector2 _direction;

        /// <param name="start">World space position</param>
        /// <param name="target">World space position</param>
        protected virtual void Intialize(Vector2 start, Vector2 target)
        {
            transform.position = start;
            _direction = (target - start).normalized;
            _timer.Initialize(time: 1f, callback: () => InvokeRemove?.Invoke(this)).Play();
        }

        protected virtual void OnHit(GameObject gameObject)
            =>InvokeHit?.Invoke(this, gameObject);

        [Inject]
        protected virtual void Construct(ProjectileMoveHadler move)
        {
            _moveHandler = move;
        }

        protected virtual void Awake()
        {
            _moveHandler.InvokeCollision += OnHit;
        }

        protected virtual void FixedUpdate()
        {
            _moveHandler.Move(_direction.x);
        }

        protected virtual void OnDestroy()
        {
            _moveHandler.InvokeCollision -= OnHit;
        }


        public class Pool : MonoMemoryPool<Vector2, Vector2, Projectile>
        {
            protected Transform _buffer;

            [Inject]
            private void Construct(ProjectileBuffer buffer)
            {
                _buffer = buffer.transform;
            }

            protected override void OnCreated(Projectile item)
            {
                item.transform.SetParent(_buffer);
                base.OnCreated(item);
            }

            /// <param name="start">World space position</param>
            /// <param name="target">World space position</param>
            protected override void Reinitialize(Vector2 start, Vector2 target, Projectile item)
            {
                base.Reinitialize(start, target, item);
                item.Intialize(start, target);
            }
        }
    }
}