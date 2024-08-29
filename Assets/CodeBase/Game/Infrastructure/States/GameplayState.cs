using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Enemy;
using Presentation.View;

namespace Game.Infrastructure.States
{
    public class GameplayState : AbstractState ,IState
    {
        private readonly IWindowFsm _windowFsm;
        private readonly EnemySpawner _enemySpawner;


        public GameplayState(IGameStateMachine gameStateMachine,
            IWindowFsm windowFsm,
            EnemySpawner enemySpawner) : base(gameStateMachine)
        {
            _windowFsm = windowFsm;
            _enemySpawner = enemySpawner;
        }

        public void OnEnter()
        {
            _windowFsm.OpenWindow(typeof(AmmoCountsView), inHistory: true);
            _enemySpawner.BeginSpawn();
            
        }

        public override void OnExit()
        {
            _enemySpawner.StopSpawn();
        }


    }
}