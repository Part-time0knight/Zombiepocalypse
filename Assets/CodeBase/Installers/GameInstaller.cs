using Game.Enemy;
using Game.Projectiles;
using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Settings _settings;

    public override void InstallBindings()
    {
        InstallPools();
        InstallServices();
    }

    private void InstallPools()
    {
        Container.Bind<ProjectileBuffer>().
            FromComponentInNewPrefab(_settings.ProjectileBuffer).AsSingle();
        Container.BindMemoryPool<Projectile, Projectile.Pool>()
            .FromComponentInNewPrefab( _settings.ProjectilePrefab);
        Container.Bind<EnemyBuffer>().
            FromComponentInNewPrefab(_settings.EnemyBuffer).AsSingle();
        Container.BindMemoryPool<EnemyHandler, EnemyHandler.Pool>()
            .FromComponentInNewPrefab(_settings.EnemyPrefab);

    }

    private void InstallServices()
    {
        Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
    }


    [Serializable]
    public class Settings
    {
        [field: SerializeField] public ProjectileBuffer ProjectileBuffer { get; private set; }
        [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }
        [field: SerializeField] public EnemyBuffer EnemyBuffer { get; private set; }
        [field: SerializeField] public EnemyHandler EnemyPrefab { get; private set; }
    }
}