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

        private PlayerDamageHandler _damageHandler;
        private PlayerMoveHandler _moveHandler;
        private PlayerShootHandler _shootHandler;
        private PlayerShootHandler.PlayerSettings _shootSettings;
        private IGameStateMachine _fsm;

        public int Ammo => _shootSettings.CurrentAmmo;

        public void TakeDamage(int damage)
        {
            _damageHandler.TakeDamage(damage);
            Debug.Log("damage");
        }

        public void PlayerReset()
        {
            _fsm.Enter<Initialize>();
            _damageHandler.Reset();
            _moveHandler.Reset();
            _shootHandler.Reset();
            OnShoot();
        }

        public void Death()
        {
            _fsm.Enter<Dead>();
        }

        [Inject]
        private void Construct(PlayerFsm fsm,
            PlayerDamageHandler damageHandler,
            PlayerMoveHandler moveHandler,
            PlayerShootHandler shootHandler,
            PlayerShootHandler.PlayerSettings shootSettings)
        {
            _fsm = fsm;
            _damageHandler = damageHandler;
            _moveHandler = moveHandler;
            _shootHandler = shootHandler;
            _shootSettings = shootSettings;
        }

        private void OnShoot()
            => InvokeShoot?.Invoke();

        private void Awake()
        {
            _shootHandler.InvokeShoot += OnShoot;
        }

        private void OnDestroy()
        {
            _shootHandler.InvokeShoot -= OnShoot;
        }
    }
}