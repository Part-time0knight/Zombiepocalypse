using Core.MVVM.View;
using Presentation.ViewModel;
using TMPro;
using UnityEngine;
using Zenject;

namespace Presentation.View
{
    public class AmmoCountsView : AbstractPayloadView<AmmoCountsViewModel>
    {
        [SerializeField] private TMP_Text _ammoText;

        [Inject]
        protected override void Construct(AmmoCountsViewModel viewModel)
        {
            base.Construct(viewModel);
            _viewModel.InvokeUpdate += UpdateAmmo;
        }

        private void OnDestroy()
        {
            _viewModel.InvokeUpdate -= UpdateAmmo;
        }

        private void UpdateAmmo(int ammo)
        {
            _ammoText.text = ammo.ToString();
        }
    }
}