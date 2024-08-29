using Game.Handlers;
using System;

namespace Game.Enemy
{
    public class EnemyDamageHandler : DamageHandler
    {
        public Action InvokeHitPointChange;

        private EnemySettings _enemySettings;

        public int CurrentHits => _enemySettings.CurrentHitPoints;
        public int Hits => _enemySettings.PresetHits;

        public EnemyDamageHandler(EnemySettings settings) : base(new EnemySettings(settings))
        {
            _enemySettings = (EnemySettings)_settings;
            
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            InvokeHitPointChange?.Invoke();
        }

        public void Reset(EnemyHandler.EnemyPreset preset)
        {
            _settings.CurrentHitPoints = preset.Hits;
            _enemySettings.PresetHits = preset.Hits;
            InvokeHitPointChange?.Invoke();
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