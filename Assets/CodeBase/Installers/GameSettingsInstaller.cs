using Game.Player;
using System;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private PlayerSettings _playerSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_playerSettings.Move); 
            Container.BindInstance(_playerSettings.Hits);
        }

        [Serializable]
        public class PlayerSettings
        {
            public PlayerMoveHandler.PlayerSettings Move;
            public PlayerDamageHandler.PlayerSettings Hits;
        }

    }
}