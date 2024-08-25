using Game.Misc;
using Game.Projectiles;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShootHandler : IInitializable
{
    protected readonly Projectile.Pool _projectilePool;
    protected readonly Settings _settings;
    protected readonly Timer _timer;
    protected readonly Transform _weaponPoint;

    protected Projectile _currentProjectile;

    public ShootHandler(Projectile.Pool pool, Settings settings, Transform weaponPoint)
    {
        _projectilePool = pool;
        _settings = settings;
        _settings.CanAttack = true;
        _settings.CurrentAttackDelay = _settings.AttackDelay;
        _settings.CurrentDamage = _settings.Damage;
        _timer = new();
        _weaponPoint = weaponPoint;
    }

    public virtual void Initialize()
    {
        _timer.Initialize(time: 0f, step: 0f).Play();
    }

    public virtual void Shoot(Vector2 target)
    {
        _currentProjectile = _projectilePool.Spawn(_weaponPoint.position, target);
        _currentProjectile.InvokeHit += OnHit;
        _currentProjectile.InvokeRemove += OnRemove;
        _settings.CanAttack = false;
        _timer.Initialize(_settings.CurrentAttackDelay,
            callback: () => _settings.CanAttack = true).Play();
    }

    protected virtual void OnHit(Projectile projectile, GameObject target)
    {
        projectile.InvokeHit -= OnHit;
        OnRemove(projectile);
    }

    protected virtual void OnRemove(Projectile projectile)
    {
        projectile.InvokeRemove -= OnRemove;
        _projectilePool.Despawn(projectile);
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] public float AttackDelay { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }

        public float CurrentAttackDelay { get; set; }
        public float CurrentDamage { get; set; }

        public bool CanAttack { get; set; }

        /// <summary>
        /// Tag of Game Object that belongs to the owner of the Shoothandler.
        /// </summary>
        public string OwnerTag { get; set; }
    }
}
