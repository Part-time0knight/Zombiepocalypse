using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;

namespace Game.Player.Fsm.States
{
    public class Initialize : AbstractState, IState
    {
        public Initialize(IGameStateMachine gameStateMachine) : base(gameStateMachine)
        {
        }

        public void OnEnter()
        {
            GameStateMachine.Enter<Idle>();
        }

        public override void OnExit()
        {

        }


    }
}