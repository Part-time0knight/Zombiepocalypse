using Core.Infrastructure.GameFsm;
using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Infrastructure.States;
using Presentation.View;
using System;

namespace Presentation.ViewModel
{
    public class GameOverViewModel : AbstractViewModel
    {
        private readonly IGameStateMachine _gameStateMachine;

        protected override Type Window => typeof(GameOverView);


        public GameOverViewModel(IWindowFsm windowFsm,
            IGameStateMachine gameStateMachine) : base(windowFsm)
        {
            _gameStateMachine = gameStateMachine;
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow();
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: true);
        }

        public void Restart()
        {
            _gameStateMachine.Enter<Gameplay>();
        }
    }
}