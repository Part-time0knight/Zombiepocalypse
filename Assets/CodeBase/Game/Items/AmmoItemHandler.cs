using Game.Handlers;
using System;


namespace Game.Items
{
    public class AmmoItemHandler : ItemHandler<AmmoItemHandler.AmmoSettings>
    {


        protected override void MakeBonus()
            => _playerHandler.Ammo += UnityEngine.Random.Range(_settings.Bonus.x, _settings.Bonus.y);

        [Serializable]
        public class AmmoSettings : Settings
        {

        }
    }
}