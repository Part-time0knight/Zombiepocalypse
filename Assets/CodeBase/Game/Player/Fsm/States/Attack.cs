using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.StaticData;
using UnityEngine;

namespace Game.Player.Fsm.States
{
    public class Attack : Hitable
    {
        private readonly PlayerInput _playerInput;
        private readonly Animator _animator;
        private readonly PlayerShootHandler _shootHandler;

        public Attack(IGameStateMachine gameStateMachine,
            PlayerDamageHandler.PlayerSettings damageSettings,
            PlayerHandler playerHandler,
            PlayerShootHandler shootHandler,
            PlayerInput playerInput,
            Animator animator) : base(gameStateMachine, damageSettings, playerHandler)
        {
            _playerInput = playerInput;
            _animator = animator;
            _shootHandler = shootHandler;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _animator.Play(AnimationsNames.Shoot);
            _playerInput.InvokeFireButtonUp += OnEndShoot;
            _shootHandler.StartAutomatic();
        }

        public override void OnExit()
        {
            base.OnExit();
            _playerInput.InvokeFireButtonUp -= OnEndShoot;
            _shootHandler.StopAutomatic();
        }

        private void OnEndShoot()
        {
            GameStateMachine.Enter<Idle>();
        }
    }
}