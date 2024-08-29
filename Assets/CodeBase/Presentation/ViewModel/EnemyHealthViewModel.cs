using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Enemy;
using Presentation.View;
using System;

namespace Presentation.ViewModel
{
    public class EnemyHealthViewModel : AbstractViewModel
    {
        public event Action<float> InvokeHits;

        private readonly EnemyDamageHandler _damageHandler;
        private readonly EnemyDamageHandler.EnemySettings _settings;

        protected override Type Window => typeof(EnemyHealthView);

        public EnemyHealthViewModel(IWindowFsm windowFsm,
            EnemyDamageHandler damageHandler,
            EnemyDamageHandler.EnemySettings settings) : base(windowFsm)
        {
            _settings = settings;
            _damageHandler = damageHandler;
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow();
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: true);
        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
            _damageHandler.InvokeHitPointChange += Update;
            Update(_settings.CurrentHitPoints);
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
            _damageHandler.InvokeHitPointChange -= Update;
        }

        private void Update(int hits)
        {
            InvokeHits?.Invoke((float)hits / _settings.PresetHits);
        }
    }
}