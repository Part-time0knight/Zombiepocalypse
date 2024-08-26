using Game.Handlers;
using System;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class EnemyAnimation : AnimationHandler, IInitializable, IDisposable
    {
        private readonly EnemyMoveHandler _moveHandler;
        private readonly Settings _settings;

        public EnemyAnimation(Animator animator,
            SpriteRenderer spriteRenderer,
            EnemyMoveHandler moveHandler,
            Settings settings) : base(animator, spriteRenderer)
        {
            _moveHandler = moveHandler;
            _settings = new(settings);
            _settings.CurrentClip = _settings.Clip;
        }

        public void Initialize()
        {
            _moveHandler.InvokeMove += FlipByDirection;
            Disable();
        }

        public void Dispose()
        {
            _moveHandler.InvokeMove -= FlipByDirection;
        }

        public void Reset(EnemyHandler.EnemyPreset preset)
        {
            _spriteRenderer.gameObject.SetActive(true);
            _settings.CurrentClip = preset.Clip;
            _animator.Play(_settings.CurrentClip.name);
            _spriteRenderer.color = new(1f, 1f, 1f, 1f);

        }

        public void Disable()
        {
            _spriteRenderer.color = new(1f, 1f, 1f, 0f);
            _spriteRenderer.gameObject.SetActive(false);
        }

        private void FlipByDirection(float direction)
        {
            Flip flip = direction < 0 ? Flip.Left : Flip.Right;
            FlipImage(flip);
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public AnimationClip Clip { get; private set; }

            public AnimationClip CurrentClip { get; set; }

            public Settings() { }
            public Settings(Settings settings) 
            {
                Clip = settings.Clip;
            }
        }
    }
}