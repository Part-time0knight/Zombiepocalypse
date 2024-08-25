using Game.Handlers;
using System;

namespace Game.Enemy
{
    public class EnemyDamageHandler : DamageHandler
    {
        public EnemyDamageHandler(EnemySettings settings) : base(settings)
        {
        }

        [Serializable]
        public class EnemySettings : Settings
        { }
    }
}