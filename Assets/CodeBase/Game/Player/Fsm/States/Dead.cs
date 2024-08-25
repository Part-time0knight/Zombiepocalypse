using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;

namespace Game.Player.Fsm.States
{
    public class Dead : AbstractState, IState
    {
        public Dead(IGameStateMachine gameStateMachine) : base(gameStateMachine)
        {
        }

        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}