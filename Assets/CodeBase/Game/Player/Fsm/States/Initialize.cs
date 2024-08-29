using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Game.Handlers;

namespace Game.Player.Fsm.States
{
    public class Initialize : AbstractState, IState
    {
        private readonly PlayerMoveHandler _moveHandler;
        private readonly PlayerDamageHandler _damageHandler;
        private readonly PlayerShootHandler _shootHandler;

        public Initialize(IGameStateMachine gameStateMachine,
            PlayerMoveHandler moveHandler,
            PlayerDamageHandler damageHandler,
            PlayerShootHandler shootHandler) : base(gameStateMachine)
        {
            _moveHandler = moveHandler;
            _damageHandler = damageHandler;
            _shootHandler = shootHandler;
        }

        public void OnEnter()
        {
            _damageHandler.Reset();
            _moveHandler.Reset();
            _shootHandler.Reset();
            GameStateMachine.Enter<Idle>();
        }

        public override void OnExit()
        {

        }


    }
}