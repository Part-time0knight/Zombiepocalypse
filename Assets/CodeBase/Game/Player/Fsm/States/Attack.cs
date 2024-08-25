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

        public Attack(IGameStateMachine gameStateMachine,
            PlayerInput playerInput,
            Animator animator) : base(gameStateMachine)
        {
            _playerInput = playerInput;
            _animator = animator;
        }

        public void OnEnter()
        {
            _animator.Play(AnimationNames.Shoot);
            _playerInput.InvokeFireButtonUp += OnEndShoot;
        }

        public override void OnExit()
        {
            _playerInput.InvokeFireButtonUp -= OnEndShoot;
        }

        private void OnEndShoot()
        {
            GameStateMachine.Enter<Idle>();
        }
    }
}