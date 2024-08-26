using System;
using UnityEngine;

namespace Game.Handlers
{
    public abstract class DamageHandler
    {
        protected readonly Settings _settings;

        public DamageHandler(Settings settings) 
        { 
            _settings = settings;
            _settings.CurrentHitPoints = _settings.HitPoints;
        }

        public virtual void TakeDamage(int damage)
        {
            _settings.CurrentHitPoints -= damage;
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public int HitPoints { get; protected set; }

            public int CurrentHitPoints { get; set; }

            public Settings() { }

            public Settings(int hitPoints, int currentHitPoints)
            {
                HitPoints = hitPoints;
                CurrentHitPoints = currentHitPoints;
            }

            public Settings(Settings settings) : this(
                settings.HitPoints,
                settings.CurrentHitPoints)
            { }
        }
    }
}