using Game.Handlers;
using System;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class EnemyAnimation : AnimationHandler, IInitializable, IDisposable
    {
        private readonly EnemyMoveHandler _moveHandler;
        public EnemyAnimation(Animator animator,
            SpriteRenderer spriteRenderer,
            EnemyMoveHandler moveHandler) : base(animator, spriteRenderer)
        {
            _moveHandler = moveHandler;
        }

        public void Initialize()
        {
            _moveHandler.InvokeMove += FlipByDirection;
        }

        public void Dispose()
        {
            _moveHandler.InvokeMove -= FlipByDirection;
        }

        private void FlipByDirection(float direction)
        {
            Flip flip = direction < 0 ? Flip.Left : Flip.Right;
            FlipImage(flip);
        }
    }
}