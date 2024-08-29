using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Player;
using Presentation.View;
using System;

namespace Presentation.ViewModel
{
    public class AmmoCountsViewModel : AbstractViewModel
    {
        public event Action<int> InvokeUpdate;
        private readonly PlayerHandler _player;

        protected override Type Window => typeof(AmmoCountsView);

        public AmmoCountsViewModel(IWindowFsm windowFsm, PlayerHandler player) : base(windowFsm)
        {
            _player = player;
        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
            _player.InvokeShoot += Update;
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
            _player.InvokeShoot -= Update;
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow();
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: true);
        }

        private void Update()
        {
            InvokeUpdate?.Invoke(_player.Ammo);
        }
    }
}