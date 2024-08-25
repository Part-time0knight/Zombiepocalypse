using Game.Domain.Factories;
using Zenject;

namespace Game.Player.Fsm
{
    public class PlayerStatesFactory : StatesFactory
    {
        public PlayerStatesFactory(DiContainer container) : base(container)
        {
        }
    }
}