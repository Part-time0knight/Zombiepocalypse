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

        [Inject]
        protected override void Construct(GameOverViewModel viewModel)
        {
            base.Construct(viewModel);
            _restartButton.onClick.AddListener(_viewModel.Restart);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(_viewModel.Restart);
        }
    }
}