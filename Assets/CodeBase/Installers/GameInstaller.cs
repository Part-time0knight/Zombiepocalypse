using Core.MVVM.Windows;
using Game.Domain.Factories;
using Game.Enemy;
using Game.Infrastructure;
using Game.Items;
using Game.Projectiles;
using Presentation.ViewModel;
using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Settings _settings;

    public override void InstallBindings()
    {
        InstallFactories();
        InstallPools();
        InstallServices();
        InstallViewModels();
    }

    private void InstallFactories()
    {
        Container.BindInterfacesAndSelfTo<StatesFactory>()
            .AsSingle()
            .NonLazy();
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

        Container.Bind<ItemsBuffer>().
            FromComponentInNewPrefab(_settings.ItemsBuffer).AsSingle();
        Container.BindMemoryPool<AmmoItemHandler, AmmoItemHandler.Pool>()
            .FromComponentInNewPrefab(_settings.AmmoItemPrefab);
    }


    private void InstallServices()
    {
        Container.BindInterfacesAndSelfTo<EnemySpawner>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<GameFsm>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<WindowFsm>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<ItemSpawner>()
            .AsSingle()
            .NonLazy();

    }

    private void InstallViewModels()
    {
        Container.BindInterfacesAndSelfTo<AmmoCountsViewModel>()
            .AsSingle()
            .NonLazy();
        Container.BindInterfacesAndSelfTo<GameOverViewModel>()
            .AsSingle()
            .NonLazy();
    }


    [Serializable]
    public class Settings
    {
        [field: SerializeField] public ProjectileBuffer ProjectileBuffer { get; private set; }
        [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }

        [field: SerializeField] public EnemyBuffer EnemyBuffer { get; private set; }
        [field: SerializeField] public EnemyHandler EnemyPrefab { get; private set; }

        [field: SerializeField] public ItemsBuffer ItemsBuffer { get; private set; }
        [field: SerializeField] public AmmoItemHandler AmmoItemPrefab { get; private set; }
    }
}