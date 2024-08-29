using Core.Infrastructure.GameFsm.States;
using Core.Infrastructure.GameFsm;
using Core.MVVM.Windows;
using Presentation.View;
using Game.Player;

namespace Game.Infrastructure.States
{
    public class GameInitialize : AbstractState, IState
    {
        private readonly IWindowResolve _windowResolve;

        public GameInitialize(IGameStateMachine gameStateMachine,
            IWindowResolve windowResolve) : base(gameStateMachine)
        {
            _windowResolve = windowResolve;
        }

        public void OnEnter()
        {
            WindowsResolve();
            GameStateMachine.Enter<Gameplay>();
        }

        public override void OnExit()
        {
        }
        private void WindowsResolve()
        {
            _windowResolve.CleanUp();
            _windowResolve.Set<AmmoCountsView>();
            _windowResolve.Set<GameOverView>();
        }
    }
}