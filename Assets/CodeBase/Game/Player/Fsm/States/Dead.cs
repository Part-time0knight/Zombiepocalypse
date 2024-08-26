using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Infrastructure;
using Game.Infrastructure.States;
using Game.StaticData;

namespace Game.Player.Fsm.States
{
    public class Dead : AbstractState, IState
    {
        private readonly PlayerAnimationHandler _animator;

        private readonly IGameStateMachine _sceneStateMachine ;

        public Dead(IGameStateMachine gameStateMachine,
            GameFsm sceneStateMachine,
            PlayerAnimationHandler animator) : base(gameStateMachine)
        {
            _animator = animator;
            _sceneStateMachine = sceneStateMachine;
        }

        public void OnEnter()
        {
            _animator.Play(AnimationsNames.Idle);
            _sceneStateMachine.Enter<GameOverState>();
        }

        public override void OnExit()
        {

        }
    }
}