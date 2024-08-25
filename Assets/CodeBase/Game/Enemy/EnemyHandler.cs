using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class EnemyHandler : MonoBehaviour
    {
        private EnemyDamageHandler _damageHandler;

        public void TakeDamage(int damage)
        {
            _damageHandler.TakeDamage(damage);
        }

        [Inject]
        private void Construct(EnemyDamageHandler damageHandler)
        {
            _damageHandler = damageHandler;
        }
    }
}