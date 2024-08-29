using Core.MVVM.View;
using Presentation.ViewModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Presentation.View
{
    public class GameOverView : AbstractPayloadView<GameOverViewModel>
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        [Inject]
        protected override void Construct(GameOverViewModel viewModel)
        {
            base.Construct(viewModel);
            _restartButton.onClick.AddListener(_viewModel.Restart);
            _exitButton.onClick.AddListener(_viewModel.Exit);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(_viewModel.Restart);
            _exitButton.onClick.RemoveListener(_viewModel.Exit);
        }
    }
}