using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Enemy;
using Game.Items;
using Game.Player;
using Presentation.View;

namespace Game.Infrastructure.States
{
    public class Gameplay : AbstractState, IState
    {
        private readonly IWindowFsm _windowFsm;
        private readonly EnemySpawner _enemySpawner;
        private readonly PlayerHandler _playerHandler;
        private readonly ItemSpawner _itemSpawner;


        public Gameplay(IGameStateMachine gameStateMachine,
            IWindowFsm windowFsm,
            EnemySpawner enemySpawner,
            PlayerHandler playerHandler,
            ItemSpawner itemSpawner) : base(gameStateMachine)
        {
            _windowFsm = windowFsm;
            _enemySpawner = enemySpawner;
            _playerHandler = playerHandler;
            _itemSpawner = itemSpawner;
        }

        public void OnEnter()
        {
            _windowFsm.OpenWindow(typeof(AmmoCountsView), inHistory: true);
            _enemySpawner.BeginSpawn();
            _itemSpawner.Reset();
            _playerHandler.InvokeShoot += AmmoCheck;
            _playerHandler.InvokeHit += HitsCheck;
        }

        public override void OnExit()
        {
            _enemySpawner.StopSpawn();
            _playerHandler.InvokeShoot -= AmmoCheck;
            _playerHandler.InvokeHit -= HitsCheck;
        }

        private void AmmoCheck()
        {
            if (_playerHandler.Ammo > 0)
                return;
            GameStateMachine.Enter<GameOver>();
        }

        private void HitsCheck()
        {
            if (_playerHandler.Hits > 0)
                return;
            GameStateMachine.Enter<GameOver>();
        }
    }
}