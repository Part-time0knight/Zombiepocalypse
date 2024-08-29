using Game.Enemy;
using Presentation.ViewModel;
using System;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            InstallEnemyComponents();
            InstallFsm();
            InstallViewModels();
        }

        private void InstallEnemyComponents()
        {
            Container.BindInstance(_settings.Body).AsSingle();
            Container.BindInstance(_settings.Animator).AsSingle();
            Container.BindInstance(_settings.SpriteRenderer).AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyMoveHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyDamageHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyAnimation>().AsSingle();
        }

        private void InstallViewModels()
        {
            Container.BindInterfacesAndSelfTo<EnemyHealthViewModel>().AsSingle();
        }

        private void InstallFsm()
        {
            Container.BindInterfacesAndSelfTo<EnemyWindowFsm>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Rigidbody2D Body { get; private set; }
            [field: SerializeField] public Animator Animator { get; private set; }
            [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        }
    }
}