using Game.Player;
using Game.Player.Fsm;
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
            InstallFactories();
            InstallComponents();
            InstallFsm();
        }

        private void InstallFactories()
        {
            Container.BindInterfacesAndSelfTo<PlayerStatesFactory>().AsSingle();
        }

        private void InstallFsm()
        {
            Container.BindInterfacesAndSelfTo<PlayerFsm>().AsSingle();
        }

        private void InstallComponents()
        {
            Container.BindInstance(_settings.RigidBody);
            Container.BindInstance(_settings.Animator);
            Container.BindInstance(_settings.Renderer);

            Container.BindInterfacesAndSelfTo<PlayerDamageHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Rigidbody2D RigidBody { get; private set; }
            [field: SerializeField] public Animator Animator { get; private set; }

            [field: SerializeField] public SpriteRenderer Renderer { get; private set; }
        }
    }
}