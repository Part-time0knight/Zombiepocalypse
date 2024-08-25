using Game.Handlers;
using System;
using UnityEngine;

namespace Game.Player
{
    public class PlayerAnimationHandler : AnimationHandler
    {
        public event Action<Flip> InvokeFlip;



        public PlayerAnimationHandler(Animator animator,
            SpriteRenderer spriteRenderer) : base(animator, spriteRenderer)
        {
        }

        public override void FlipImage(Flip flip)
        {
            base.FlipImage(flip);
            InvokeFlip?.Invoke(flip);
        }
    }
}