using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.StaticData;
using UnityEngine;

namespace Game.Player.Fsm.States
{
    public class Idle : AbstractState, IState
    {
        private readonly PlayerInput _playerInput;
        private readonly Animator _animator;

        public Idle(IGameStateMachine gameStateMachine, PlayerInput playerInput, Animator animator) : base(gameStateMachine)
        {
            _playerInput = playerInput;
            _animator = animator;
        }

        public void OnEnter()
        {
            _animator.Play(AnimationNames.Idle);
            _playerInput.InvokeHorizontal += OnMove;
            _playerInput.InvokeFireButtonDown += OnAttack;
        }

        public override void OnExit()
        {
            _playerInput.InvokeHorizontal -= OnMove;
            _playerInput.InvokeFireButtonDown -= OnAttack;
        }

        private void OnMove(float direction)
        {
            if (direction == 0f)
                return;
            GameStateMachine.Enter<Run>();
        }

        private void OnAttack()
        {
            GameStateMachine.Enter<Attack>();
        }
    }
}