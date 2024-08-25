using UnityEngine;

namespace Game.Handlers
{
    public class AnimationHandler
    {
        protected readonly Animator _animator;
        protected readonly SpriteRenderer _spriteRenderer;

        public AnimationHandler(Animator animator, SpriteRenderer spriteRenderer)
        {
            _animator = animator;
            _spriteRenderer = spriteRenderer;
        }

        public virtual void Play(string clipName)
            => _animator.Play(clipName);

        public virtual void FlipImage(Flip flip)
            => _spriteRenderer.flipX = flip == 0 ? true : false;

        public enum Flip
        {
            Left = 0,
            Right = 1,
        }
    }
}