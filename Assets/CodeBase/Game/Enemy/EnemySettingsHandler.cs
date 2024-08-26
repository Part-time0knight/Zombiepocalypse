
namespace Game.Enemy
{
    public class EnemySettingsHandler
    {
        private readonly EnemyMoveHandler.EnemySettings _moveSettings;
        private readonly EnemyDamageHandler.EnemySettings _damageSettings;

        public EnemyMoveHandler.EnemySettings MoveSettings => _moveSettings;
        public EnemyDamageHandler.EnemySettings DamageSettings => _damageSettings;

        public EnemySettingsHandler(EnemyMoveHandler.EnemySettings moveSettings, EnemyDamageHandler.EnemySettings damageSettings)
        {
            _moveSettings = new(moveSettings);
            _damageSettings = new(damageSettings);
        }
    }
}