using Game.Handlers;
using Game.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class EnemyMoveHandler : MoveHandler, IFixedTickable, IInitializable
    {
        public event Action<float> InvokeMove;

        private readonly PlayerMoveHandler.PlayerSettings _playerSettings;
        private readonly List<Collider2D> _colliders = new();

        private float _direction;

        public EnemyMoveHandler(Rigidbody2D body,
            EnemySettings settings,
            PlayerMoveHandler.PlayerSettings playerSettings) : base(body, new(settings))
        {
            _playerSettings = playerSettings;
            
        }
        public void Initialize()
        {
            _body.GetAttachedColliders(_colliders);
            Disable();
        }

        public void FixedTick()
        {
            _direction = (_playerSettings.CurrentPosition - _body.position).normalized.x;
            Move(_direction);
            InvokeMove?.Invoke(_direction);
        }

        public void Reset(EnemyHandler.EnemyPreset preset, Vector2 spawn)
        {
            _body.position = spawn;
            _settings.CurrentSpeed = preset.Speed;
            _colliders.ForEach((collider)=> collider.enabled = true);
        }

        public void Disable()
        {
            _colliders.ForEach((collider) => collider.enabled = false);
            _body.transform.localPosition = Vector2.zero;
        }

  

        [Serializable]
        public class EnemySettings : Settings
        {
            public EnemySettings(EnemySettings enemySettings) : base(enemySettings) 
            { }
        }
    }
}