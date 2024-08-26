using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.StaticData;

namespace Game.Player.Fsm.States
{
    public class Idle : Hitable
    {
        private readonly PlayerInput _playerInput;
        private readonly PlayerAnimationHandler _animator;

        public Idle(IGameStateMachine gameStateMachine,
            PlayerDamageHandler.PlayerSettings damageSettings,
            PlayerShootHandler.PlayerSettings playerSettings,
            PlayerInput playerInput,
            PlayerAnimationHandler animator) : base(gameStateMachine, damageSettings, playerSettings)
        {
            _playerInput = playerInput;
            _animator = animator;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _animator.Play(AnimationsNames.Idle);
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