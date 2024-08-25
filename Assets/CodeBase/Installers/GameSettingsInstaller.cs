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
        [SerializeField] private ProjectileSettings _ProjectileSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_playerSettings.Move); 
            Container.BindInstance(_playerSettings.Hits);
            Container.BindInstance(_playerSettings.Shoot);

            Container.BindInstance(_ProjectileSettings.Move);
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
    }
}