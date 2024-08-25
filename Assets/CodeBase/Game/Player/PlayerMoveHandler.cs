using Game.Handlers;
using System;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerMoveHandler : MoveHandler
    {

        public PlayerMoveHandler(Rigidbody2D body, PlayerSettings settings) : base(body, settings)
        {
        }

        [Serializable]
        public class PlayerSettings : Settings
        {

        }
    }
}