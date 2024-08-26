using Core.Infrastructure.GameFsm.States;
using Core.Infrastructure.GameFsm;
using Core.MVVM.Windows;
using Game.Infrastructure.States;
using Presentation.View;

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
        GameStateMachine.Enter<GameplayState>();
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

