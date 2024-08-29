using Game.Handlers;
using System;

namespace Game.Enemy
{
    public class EnemyDamageHandler : DamageHandler
    {
        public Action<int> InvokeHitPointChange;

        public EnemyDamageHandler(EnemySettings settings) : base(new(settings))
        {
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            InvokeHitPointChange?.Invoke(_settings.CurrentHitPoints);
        }

        public void Reset(EnemyHandler.EnemyPreset preset)
        {
            _settings.CurrentHitPoints = preset.Hits;
            (_settings as EnemySettings).PresetHits = preset.Hits;
        }

        [Serializable]
        public class EnemySettings : Settings
        {
            public int PresetHits;

            public EnemySettings(EnemySettings settings) : base(settings)
            {
                
            }
        }
    }
}