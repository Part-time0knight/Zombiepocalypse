using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Player;
using Presentation.View;

namespace Game.Infrastructure.States
{
    public class GameOver : AbstractState, IState
    {
        private readonly IWindowFsm _windowFsm;
        private readonly PlayerHandler _playerHandler;

        public GameOver(IGameStateMachine gameStateMachine,
            IWindowFsm windowFsm, PlayerHandler playerHandler) : base(gameStateMachine)
        {
            _windowFsm = windowFsm;
            _playerHandler = playerHandler;
        }

        public void OnEnter()
        {
            _windowFsm.OpenWindow(typeof(GameOverView), inHistory: true);
            _playerHandler.Death();
        }

        public override void OnExit()
        {
            _playerHandler.PlayerReset();
        }
    }
}