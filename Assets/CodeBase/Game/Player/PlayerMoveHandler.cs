using Game.Handlers;
using System;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerMoveHandler : MoveHandler, IFixedTickable
    {
        private float _axis;

        public PlayerMoveHandler(Rigidbody2D body, PlayerSettings settings) : base(body, settings)
        {
        }

        public void FixedTick()
        {
            _axis = Input.GetAxis("Horizontal");
            Move(_axis);
        }

        [Serializable]
        public class PlayerSettings : Settings
        {

        }
    }
}