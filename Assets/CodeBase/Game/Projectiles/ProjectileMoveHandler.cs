using Game.Handlers;
using System;
using UnityEngine;

namespace Game.Projectiles
{
    public class ProjectileMoveHadler : MoveHandler
    {
        public Action<GameObject> InvokeCollision;

        public ProjectileMoveHadler(Rigidbody2D body, ProjectileSettings settings) : base(body, settings)
        {
            _filter.useTriggers = true;
        }

        protected override float Collision(float direction)
        {
            _body.Cast(new Vector2(direction, 0), _filter, _hits, _settings.CurrentSpeed * Time.fixedDeltaTime * _collisionOffSet);
            foreach (var hit in _hits)
                InvokeCollision?.Invoke(hit.transform.gameObject);
            return direction;
        }

        [Serializable]
        public class ProjectileSettings : Settings
        { 
        }
    }
}