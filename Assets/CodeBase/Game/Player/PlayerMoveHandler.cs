using Game.Handlers;
using System;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMoveHandler : MoveHandler
    {
        private readonly PlayerSettings _playerSettings;
        public PlayerMoveHandler(Rigidbody2D body, PlayerSettings settings) : base(body, settings)
        {
            _playerSettings = settings;
            _playerSettings.CurrentPosition = _body.position;
        }

        public override void Move(float direction)
        {
            base.Move(direction);
            _playerSettings.CurrentPosition = _body.position;
        }

        [Serializable]
        public class PlayerSettings : Settings
        {
            public Vector2 CurrentPosition { get; set; }
        }
    }
}