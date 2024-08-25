using Game.Handlers;
using System;

namespace Game.Player
{
    public class PlayerDamageHandler : DamageHandler
    {
        public PlayerDamageHandler(PlayerSettings settings) : base(settings)
        {
        }

        [Serializable]
        public class PlayerSettings : Settings { }
    }
}