using Game.Player;
using System;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            InstallComponents();
        }

        private void InstallComponents()
        {
            Container.BindInstance(_settings.RigidBody);
            Container.BindInstance(_settings.Animator);

            Container.BindInterfacesAndSelfTo<PlayerDamageHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Rigidbody2D RigidBody { get; private set; }
            [field: SerializeField] public Animator Animator { get; private set; }
        }
    }
}