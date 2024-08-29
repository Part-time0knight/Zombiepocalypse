using Game.Enemy;
using Game.Items;
using Game.Player;
using Game.Projectiles;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private PlayerSettings _player;
        [SerializeField] private ProjectileSettings _projectile;
        [SerializeField] private EnemySettings _defoultEnemy;
        [SerializeField] private EnemySpawnerSettings _spawner;
        [SerializeField] private ItemSettings _item;

        public override void InstallBindings()
        {
            Container.BindInstance(_player.Move).AsSingle(); 
            Container.BindInstance(_player.Hits).AsSingle();
            Container.BindInstance(_player.Shoot).AsSingle();

            Container.BindInstance(_projectile.Move).AsSingle();

            Container.BindInstance(_defoultEnemy.Hits).AsSingle();
            Container.BindInstance(_defoultEnemy.Move).AsSingle();
            Container.BindInstance(_defoultEnemy.Animation).AsSingle();

            Container.BindInstance(_item.Ammo).AsSingle();

            Container.BindInstance(_spawner.Presets.Presets).AsSingle();
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
            public EnemyAnimation.Settings Animation;
        }

        [Serializable]
        public class EnemiesPresets
        {
            public List<EnemyHandler.EnemyPreset> Presets;
        }

        [Serializable]
        public class EnemySpawnerSettings
        {
            public EnemySpawner.Settings Spawner;

            public EnemiesPresets Presets;
        }

        [Serializable]
        public class ItemSettings
        {
            public AmmoItemHandler.AmmoSettings Ammo;
        }
    }
}