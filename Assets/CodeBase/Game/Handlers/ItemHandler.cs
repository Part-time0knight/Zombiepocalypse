using Game.Items;
using Game.Player;
using Game.StaticData;
using System;
using UnityEngine;
using Zenject;

namespace Game.Handlers
{
    public abstract class ItemHandler<TSettings> : MonoBehaviour
        where TSettings : ItemHandler<TSettings>.Settings
    {
        public event Action<ItemHandler<TSettings>> InvokeCollision;

        protected Settings _settings;
        protected PlayerHandler _playerHandler;

        [Inject]
        protected virtual void Construct(TSettings settings,
            PlayerHandler playerHandler)
        {
            _settings = settings;
            _playerHandler = playerHandler;
        }

        protected abstract void MakeBonus();

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision == null || collision.transform.tag != TagNames.Player)
                return;
            MakeBonus();
            InvokeCollision?.Invoke(this);
        }

        public class Pool : MonoMemoryPool<Vector2, ItemHandler<TSettings>>
        {
            protected Transform _buffer;

            [Inject]
            private void Construct(ItemsBuffer buffer)
            {
                _buffer = buffer.transform;
            }

            protected override void OnCreated(ItemHandler<TSettings> item)
            {
                item.transform.SetParent(_buffer);
                base.OnCreated(item);
            }

            protected override void Reinitialize(Vector2 spawn, ItemHandler<TSettings> item)
            {
                base.Reinitialize(spawn, item);
                item.transform.position = spawn;
                item.InvokeCollision += OnTake;
            }

            private void OnTake(ItemHandler<TSettings> item)
            {
                item.InvokeCollision -= OnTake;
                Despawn(item);
            }
        }

        [Serializable]
        public class Settings
        {
            /// <summary>
            /// Range for random.
            /// </summary>
            [field: SerializeField] public Vector2Int Bonus { get; private set; }

            /// <summary>
            /// Item drop chance.
            /// </summary>
            [field: SerializeField] public float Chance { get; private set; }
        }
    }
}