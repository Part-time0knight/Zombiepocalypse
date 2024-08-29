using Cysharp.Threading.Tasks;
using Game.Handlers;
using Game.Projectiles;
using System;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerShootHandler : ShootHandler, IDisposable
    {
        public event Action InvokeShoot;

        private readonly PlayerAnimationHandler _animationHandler;
        private readonly float _weaponPosition;
        private readonly PlayerSettings _playerSettings;

        private float _normal = 1f;
        private bool _breakAutomatic;

        public PlayerShootHandler(Projectile.Pool pool,
            PlayerSettings settings,
            Transform weapon, PlayerAnimationHandler animation) : base(pool, settings, weapon)
        {
            _weaponPosition = _weaponPoint.localPosition.x;
            _animationHandler = animation;
            _playerSettings = settings;
            _playerSettings.CurrentAmmo = _playerSettings.Ammo;
        }

        public override void Initialize()
        {
            base.Initialize();
            _animationHandler.InvokeFlip += OnSpriteFlip;
        }

        public void Dispose()
        {
            _animationHandler.InvokeFlip -= OnSpriteFlip;
        }

        public void Reset()
        {
            _playerSettings.CurrentAmmo = _playerSettings.Ammo;
            InvokeShoot?.Invoke();
            ResetDelay();
        }

        public void StartAutomatic()
        {
            _breakAutomatic = false;
            Repeater();
        }

        public void StopAutomatic()
        {
            _breakAutomatic = true;
        }

        public void Shoot()
        {
            var target = _weaponPoint.position;
            target.x += _normal;
            Shoot(target);
            --_playerSettings.CurrentAmmo;
            InvokeShoot?.Invoke();
        }

        private void ResetDelay()
        {
            _timer.Initialize(time: 0.5f).Play();
        }

        private async UniTask Repeater()
        {
            do
            {
                await UniTask.WaitWhile(() => _timer.Active);
                if (!_breakAutomatic)
                    Shoot();

            } while (!_breakAutomatic);
        }

        private void OnSpriteFlip(AnimationHandler.Flip flip)
        {
            var x = _weaponPosition;
            x *= flip == 0 ? -1f : 1f;
            _normal = x;
            _weaponPoint.localPosition = new(x, _weaponPoint.localPosition.y);
        }

        [Serializable]
        public class PlayerSettings : Settings
        {
            [field: SerializeField] public int Ammo { get; private set; }
            public int CurrentAmmo { get; set; }
        }
    }
}