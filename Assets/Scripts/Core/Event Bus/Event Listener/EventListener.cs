using Project.EventBusSystem;
using System;

namespace Project
{
    /// <summary>
    /// Слушатель события.
    /// </summary>
    /// <typeparam name="T">Тип события.</typeparam>
    public readonly struct EventListener<T> : IEventListener
    {
        /// <summary>
        /// Хеш слушателя.
        /// </summary>
        internal readonly int Hash;

        private readonly bool _isRemovable;
        private readonly Action<T> _action;

        /// <param name="action">Метод, вызываемый при событии.</param>
        /// <param name="isRemovable">Может ли слушатель отписаться от события досрочно.</param>
        public EventListener(Action<T> action, bool isRemovable = true)
        {
            Hash = action.Target.GetHashCode();

            _action = action;
            _isRemovable = isRemovable;
        }

        void IEventListener.PostEvent(object eventObject)
        {
            _action?.Invoke((T)eventObject);
        }

        bool IEventListener.CheckListenerByHash(int hash)
        {
            return Hash == hash;
        }

        public bool IsRemovable()
        {
            return _isRemovable;
        }
    }
}
