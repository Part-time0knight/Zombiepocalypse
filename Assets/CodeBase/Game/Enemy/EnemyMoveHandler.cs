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
            EnemySettingsHandler settings,
            PlayerMoveHandler.PlayerSettings playerSettings) : base(body, settings.MoveSettings)
        {
            _playerSettings = playerSettings;
        }

        public void FixedTick()
        {
            _direction = (_playerSettings.CurrentPosition - _body.position).normalized.x;
            Move(_direction);
            InvokeMove?.Invoke(_direction);
        }

        public void Reset(Vector2 spawn)
        {
            _body.position = spawn;
        }

        [Serializable]
        public class EnemySettings : Settings
        {
            public EnemySettings(EnemySettings enemySettings) : base(enemySettings) 
            { }
        }
    }
}