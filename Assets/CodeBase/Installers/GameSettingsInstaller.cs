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
        [SerializeField] private PlayerSettings _player;
        [SerializeField] private ProjectileSettings _projectile;
        [SerializeField] private EnemySettings _enemy;
        [SerializeField] private EnemySpawnerSettings _spawner;

        public override void InstallBindings()
        {
            Container.BindInstance(_player.Move).AsSingle(); 
            Container.BindInstance(_player.Hits).AsSingle();
            Container.BindInstance(_player.Shoot).AsSingle();

            Container.BindInstance(_projectile.Move).AsSingle();

            Container.BindInstance(_enemy.Move).AsSingle();
            Container.BindInstance(_enemy.Hits).AsSingle();

            Container.BindInstance(_spawner.Spawner).AsSingle();
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

        [Serializable]
        public class EnemySpawnerSettings
        {
            public EnemySpawner.Settings Spawner;
        }
    }
}