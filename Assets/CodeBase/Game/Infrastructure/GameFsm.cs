using Core.Domain.Factories;
using Core.Infrastructure.GameFsm;
using Game.Infrastructure.States;
using Zenject;

namespace Game.Infrastructure
{
    public class GameFsm : AbstractGameStateMachine, IInitializable
    {
        public GameFsm(IStatesFactory factory) : base(factory)
        {
        }

        public void Initialize()
        {
            StateResolve();
            Enter<GameInitialize>();
        }

        private void StateResolve()
        {
            _states.Add(typeof(GameInitialize), _factory.Create<GameInitialize>());
            _states.Add(typeof(GameplayState), _factory.Create<GameplayState>());
            _states.Add(typeof(GameOverState), _factory.Create<GameOverState>());
        }
    }
}