using Core.Infrastructure.GameFsm;
using Game.Player.Fsm;
using Game.Player.Fsm.States;
using System;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerHandler : MonoBehaviour
    {
        public event Action InvokeShoot;
        public event Action InvokeHit;

        private PlayerDamageHandler _damageHandler;
        private PlayerDamageHandler.PlayerSettings _damageSettings;
        private PlayerShootHandler _shootHandler;
        private PlayerShootHandler.PlayerSettings _shootSettings;
        private IGameStateMachine _fsm;

        public int Ammo => _shootSettings.CurrentAmmo;
        public int Hits => _damageSettings.CurrentHitPoints;

        public void TakeDamage(int damage)
        {
            _damageHandler.TakeDamage(damage);
            OnHit();
        }

        public void PlayerReset()
        {
            _fsm.Enter<Initialize>();

        }

        public void Death()
        {
            _fsm.Enter<Dead>();
        }

        [Inject]
        private void Construct(PlayerFsm fsm,
            PlayerDamageHandler damageHandler,
            PlayerShootHandler shootHandler,
            PlayerShootHandler.PlayerSettings shootSettings,
            PlayerDamageHandler.PlayerSettings damageSettings)
        {
            _fsm = fsm;
            _damageHandler = damageHandler;
            _shootHandler = shootHandler;
            _shootSettings = shootSettings;
            _damageSettings = damageSettings;
        }

        private void OnShoot()
            => InvokeShoot?.Invoke();

        private void OnHit()
            => InvokeHit?.Invoke();

        private void Awake()
        {
            _shootHandler.InvokeShoot += OnShoot;
            
        }

        private void Start()
        {
            PlayerReset();
        }

        private void OnDestroy()
        {
            _shootHandler.InvokeShoot -= OnShoot;
        }
    }
}