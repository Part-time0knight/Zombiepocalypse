using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.Fsm.States
{
    public abstract class Hitable : AbstractState, IState
    {
        private readonly PlayerDamageHandler.PlayerSettings _damageSettings;
        private readonly PlayerShootHandler.PlayerSettings _shootSettings;
        private readonly PlayerShootHandler _shootHandler;

        public Hitable(IGameStateMachine gameStateMachine,
            PlayerShootHandler shootHandler,
            PlayerDamageHandler.PlayerSettings damageSettings,
            PlayerShootHandler.PlayerSettings shootSettings) : base(gameStateMachine)
        {
            _damageSettings = damageSettings;
            _shootSettings = shootSettings;
            _shootHandler = shootHandler;
        }

        public virtual void OnEnter()
        {
            _damageSettings.InvokeHit += OnDeath;
            _shootHandler.InvokeShoot += OnShoot;
        }

        public override void OnExit()
        {
            _damageSettings.InvokeHit -= OnDeath;
            _shootHandler.InvokeShoot -= OnShoot;
        }

        private void OnDeath()
        {
            if (_damageSettings.CurrentHitPoints > 0)
                return;
            GameStateMachine.Enter<Dead>();
        }

        private void OnShoot()
        {
            if (_shootSettings.CurrentAmmo > 0)
                return;
            GameStateMachine.Enter<Dead>();
        }
    }
}