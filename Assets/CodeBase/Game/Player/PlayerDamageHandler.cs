using Game.Handlers;
using System;

namespace Game.Player
{
    public class PlayerDamageHandler : DamageHandler
    {
        private readonly PlayerSettings _playerSettings;

        public PlayerDamageHandler(PlayerSettings settings) : base(settings)
        {
            _playerSettings = settings;
        }

        public void Reset()
        {
            _playerSettings.CurrentHitPoints = _playerSettings.HitPoints;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            _playerSettings.InvokeHit?.Invoke();
        }

        [Serializable]
        public class PlayerSettings : Settings 
        {
            public Action InvokeHit;
        }
    }
}