using System;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerInput : IFixedTickable, ITickable
    {
        public event Action<float> InvokeHorizontal;
        public event Action InvokeFireButtonDown;
        public event Action InvokeFireButtonUp;

        public void FixedTick()
        {
            InvokeHorizontal?.Invoke(OnMoveHorizontal());
        }

        public void Tick()
        {
            if (OnMouseDown())
                InvokeFireButtonDown?.Invoke();
            else if (OnMouseUp())
                InvokeFireButtonUp?.Invoke();
        }

        private float OnMoveHorizontal()
            => Input.GetAxis("Horizontal");
        
        private bool OnMouseDown()
            => Input.GetMouseButtonDown(0);

        private bool OnMouseUp()
            => Input.GetMouseButtonUp(0);


    }
}