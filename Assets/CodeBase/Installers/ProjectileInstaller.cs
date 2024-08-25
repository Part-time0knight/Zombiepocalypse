using Game.Projectiles;
using System;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectileInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            Container.BindInstance(_settings.Body).AsSingle();
            Container.BindInterfacesAndSelfTo<ProjectileMoveHadler>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Rigidbody2D Body { get; private set; }
        }
    }
}