
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Handlers
{
    public abstract class MoveHandler
    {

        protected readonly Settings _settings;
        protected readonly Rigidbody2D _body;

        protected readonly List<RaycastHit2D> _hits;
       
        protected float _collisionOffSet;
        protected ContactFilter2D _filter;

        protected Vector2 _velocity
        {
            get => _body.velocity;
            set => _body.velocity = value;
        }

        public MoveHandler(Rigidbody2D body, Settings settings) 
        {
            _body = body;
            _settings = settings;
            _settings.CurrentSpeed = _settings.Speed;
            _filter = new();
            _hits = new();
            _collisionOffSet = 0.5f;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction">-1...1</param>
        public virtual void Move(float direction)
            => _velocity = new(Collision(direction) * _settings.CurrentSpeed, 0);

        protected virtual float Collision(float direction)
        {
            _body.Cast(new Vector2(direction, 0), _filter, _hits, _settings.CurrentSpeed * Time.fixedDeltaTime * _collisionOffSet);
            foreach (var hit in _hits)
                direction = hit.normal.x != 0 ? 0 : direction;
            return direction;
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float Speed { get; private set; }
            public float CurrentSpeed { get; set; }
        }
    }
}