using Game.Enemy;
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
        }

        private void InstallEnemyComponents()
        {
            Container.BindInstance(_settings.Body).AsSingle();
            Container.BindInstance(_settings.Animator).AsSingle();
            Container.BindInstance(_settings.SpriteRenderer).AsSingle();

            Container.BindInterfacesAndSelfTo<EnemySettingsHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyMoveHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyDamageHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyAnimation>().AsSingle();
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