using Game.Handlers;
using Game.Player;
using System;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class EnemyMoveHandler : MoveHandler, IFixedTickable
    {
        public event Action<float> InvokeMove;

        private readonly PlayerMoveHandler.PlayerSettings _playerSettings;
        private float _direction;

        public EnemyMoveHandler(Rigidbody2D body,
            EnemySettings settings,
            PlayerMoveHandler.PlayerSettings playerSettings) : base(body, settings)
        {
            _playerSettings = playerSettings;
        }

        public void FixedTick()
        {
            _direction = (_playerSettings.CurrentPosition - _body.position).normalized.x;
            Move(_direction);
            InvokeMove?.Invoke(_direction);
        }

        [Serializable]
        public class EnemySettings : Settings
        { }
    }
}