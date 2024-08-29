using Core.MVVM.View;
using Presentation.ViewModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Presentation.View
{
    public class EnemyHealthView : AbstractPayloadView<EnemyHealthViewModel>
    {
        [SerializeField] private Image _healthbarImage;
        
        [Inject]
        protected override void Construct(EnemyHealthViewModel viewModel)
        {
            base.Construct(viewModel);
            _viewModel.InvokeHits += UpdateHealth;
        }

        private void UpdateHealth(float health)
        {
            _healthbarImage.fillAmount = health;
        }

        private void OnDestroy()
        {
            _viewModel.InvokeHits -= UpdateHealth;
        }
    }
}