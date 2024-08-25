using Game.Enemy;
using Game.Player;
using Game.Projectiles;
using System;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private ProjectileSettings _projectileSettings;
        [SerializeField] private EnemySettings _enemySettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_playerSettings.Move); 
            Container.BindInstance(_playerSettings.Hits);
            Container.BindInstance(_playerSettings.Shoot);

            Container.BindInstance(_projectileSettings.Move);

            Container.BindInstance(_enemySettings.Move);
            Container.BindInstance(_enemySettings.Hits);
        }

        [Serializable]
        public class PlayerSettings
        {
            public PlayerMoveHandler.PlayerSettings Move;
            public PlayerDamageHandler.PlayerSettings Hits;
            public PlayerShootHandler.PlayerSettings Shoot;
        }

        [Serializable]
        public class ProjectileSettings
        {
            public ProjectileMoveHadler.ProjectileSettings Move;
        }

        [Serializable]
        public class EnemySettings
        {
            public EnemyDamageHandler.EnemySettings Hits;
            public EnemyMoveHandler.EnemySettings Move;
        }
    }
}