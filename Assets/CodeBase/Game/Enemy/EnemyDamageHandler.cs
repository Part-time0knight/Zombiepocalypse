using Game.Handlers;
using System;

namespace Game.Enemy
{
    public class EnemyDamageHandler : DamageHandler
    {
        public Action<int> InvokeHitPointChange;

        public EnemyDamageHandler(EnemySettingsHandler settings) : base(settings.DamageSettings)
        {
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            InvokeHitPointChange?.Invoke(_settings.CurrentHitPoints);
        }

        public void Reset()
        {
            _settings.CurrentHitPoints = _settings.HitPoints;
        }

        [Serializable]
        public class EnemySettings : Settings
        {
            public EnemySettings(EnemySettings settings) : base(settings)
            { }
        }
    }
}