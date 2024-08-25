using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.StaticData;
using UnityEngine;
using static Game.Handlers.AnimationHandler;

namespace Game.Player.Fsm.States
{
    public class Run : AbstractState, IState
    {
        private readonly PlayerInput _playerInput;
        private readonly PlayerMoveHandler _moveHandler;
        private readonly PlayerAnimationHandler _animator;

        public Run(IGameStateMachine gameStateMachine,
            PlayerInput playerInput,
            PlayerMoveHandler moveHandler,
            PlayerAnimationHandler animator) : base(gameStateMachine)
        {
            _playerInput = playerInput;
            _moveHandler = moveHandler;
            _animator = animator;
        }

        public void OnEnter()
        {
            _animator.Play(AnimationNames.Run);
            _playerInput.InvokeHorizontal += OnMove;
            _playerInput.InvokeFireButtonDown += OnAttack;
        }

        public override void OnExit()
        {
            _playerInput.InvokeHorizontal -= OnMove;
            _playerInput.InvokeFireButtonDown -= OnAttack;
            _moveHandler.Move(0);
        }

        private void OnMove(float direction)
        {
            if (direction == 0f)
            {
                GameStateMachine.Enter<Idle>();
                return;
            }
            _moveHandler.Move(direction);
            _animator.FlipImage(Mathf.Sign(direction) < 0 ? Flip.Left : Flip.Right);
        }

        private void OnAttack()
        {
            GameStateMachine.Enter<Attack>();
        }
    }
}