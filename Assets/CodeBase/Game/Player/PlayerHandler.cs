using Core.Infrastructure.GameFsm;
using Game.Player.Fsm;
using Game.Player.Fsm.States;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerHandler : MonoBehaviour
    {
        private PlayerDamageHandler _damageHandler;
        private PlayerMoveHandler _moveHandler;
        private PlayerShootHandler _shootHandler;
        private IGameStateMachine _fsm;

        public void TakeDamage(int damage)
        {
            _damageHandler.TakeDamage(damage);
            Debug.Log("damage");
        }

        public void PlayerReset()
        {
            _fsm.Enter<Initialize>();
            _damageHandler.Reset();
            _moveHandler.Reset();
            _shootHandler.Reset();
        }

        [Inject]
        private void Construct(PlayerFsm fsm,
            PlayerDamageHandler damageHandler,
            PlayerMoveHandler moveHandler,
            PlayerShootHandler shootHandler)
        {
            _fsm = fsm;
            _damageHandler = damageHandler;
            _moveHandler = moveHandler;
            _shootHandler = shootHandler;
        }
    }
}