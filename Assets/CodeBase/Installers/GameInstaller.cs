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
            FromComponentInNewPrefab(_settings.Buffer).AsSingle();
        Container.BindMemoryPool<Projectile, Projectile.Pool>()
            .FromComponentInNewPrefab( _settings.ProjectilePrefab);

    }

    private void InstallServices()
    {

    }


    [Serializable]
    public class Settings
    {
        [field: SerializeField] public ProjectileBuffer Buffer { get; private set; }
        [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }
    }
}