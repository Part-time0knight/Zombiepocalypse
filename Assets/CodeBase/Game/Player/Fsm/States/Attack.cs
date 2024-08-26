using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.StaticData;
using UnityEngine;

namespace Game.Player.Fsm.States
{
    public class Attack : AbstractState, IState
    {
        private readonly PlayerInput _playerInput;
        private readonly Animator _animator;
        private readonly PlayerShootHandler _shootHandler;

        public Attack(IGameStateMachine gameStateMachine,
            PlayerInput playerInput,
            Animator animator, PlayerShootHandler shootHandler) : base(gameStateMachine)
        {
            _playerInput = playerInput;
            _animator = animator;
            _shootHandler = shootHandler;
        }

        public void OnEnter()
        {
            _animator.Play(AnimationsNames.Shoot);
            _playerInput.InvokeFireButtonUp += OnEndShoot;
            _shootHandler.StartAutomatic();
        }

        public override void OnExit()
        {
            _playerInput.InvokeFireButtonUp -= OnEndShoot;
            _shootHandler.StopAutomatic();
        }

        private void OnEndShoot()
        {
            GameStateMachine.Enter<Idle>();
        }
    }
}