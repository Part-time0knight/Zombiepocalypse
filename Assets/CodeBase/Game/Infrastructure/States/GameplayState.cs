using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Presentation.View;

namespace Game.Infrastructure.States
{
    public class GameplayState : AbstractState ,IState
    {
        private readonly IWindowFsm _windowFsm;
        private readonly IWindowResolve _windowResolve;

        public GameplayState(IGameStateMachine gameStateMachine,
            IWindowFsm windowFsm,
            IWindowResolve windowResolve) : base(gameStateMachine)
        {
            _windowFsm = windowFsm;
            _windowResolve = windowResolve;
        }

        public void OnEnter()
        {
            WindowsResolve();
            _windowFsm.OpenWindow(typeof(AmmoCountsView), inHistory: true);
        }

        public override void OnExit()
        {
        }

        private void WindowsResolve()
        {
            _windowResolve.CleanUp();
            _windowResolve.Set<AmmoCountsView>();
        }
    }
}