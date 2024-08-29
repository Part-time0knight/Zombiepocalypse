using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;

namespace Game.Player.Fsm.States
{
    public abstract class Hitable : AbstractState, IState
    {
        private readonly PlayerDamageHandler.PlayerSettings _damageSettings;
        private readonly PlayerHandler _playerHandler;

        public Hitable(IGameStateMachine gameStateMachine,
            PlayerDamageHandler.PlayerSettings damageSettings,
            PlayerHandler playerHandler) : base(gameStateMachine)
        {
            _damageSettings = damageSettings;
            _playerHandler = playerHandler;
        }

        public virtual void OnEnter()
        {
            _damageSettings.InvokeHit += OnHit;
        }

        public override void OnExit()
        {
            _damageSettings.InvokeHit -= OnHit;
        }

        private void OnHit()
        {
            _playerHandler.Death();
        }
    }
}