using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.StaticData;
using UnityEngine;

namespace Game.Player.Fsm.States
{
    public class Run : AbstractState, IState
    {
        private readonly PlayerInput _playerInput;
        private readonly PlayerMoveHandler _moveHandler;
        private readonly Animator _animator;
        private readonly SpriteRenderer _spriteRenderer;

        public Run(IGameStateMachine gameStateMachine,
            PlayerInput playerInput,
            PlayerMoveHandler moveHandler,
            Animator animator, SpriteRenderer spriteRenderer) : base(gameStateMachine)
        {
            _playerInput = playerInput;
            _moveHandler = moveHandler;
            _animator = animator;
            _spriteRenderer = spriteRenderer;
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
            _spriteRenderer.flipX = Mathf.Sign(direction) < 0 ? true : false;
        }

        private void OnAttack()
        {
            GameStateMachine.Enter<Attack>();
        }
    }
}