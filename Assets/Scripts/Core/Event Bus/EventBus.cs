using Project.EventBusSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{
    /// <summary>
    /// Система событий для разделения логики и представления.
    /// </summary>
    public sealed class EventBus
    {
        /// <summary>
        /// Экземпляр <see cref="EventBus"/>.
        /// </summary>
        public static readonly EventBus Instance = new EventBus();

        /// <summary>
        /// Подписывает слушателя на событие.
        /// </summary>
        internal readonly Action<Type, IEventListener> RegisterListenerEvent;
        /// <summary>
        /// Отписывает слушателя от события.
        /// </summary>
        internal readonly Action<Type, int> UnregisterListenerEvent;
        /// <summary>
        /// Отправляет событие.
        /// </summary>
        internal readonly Action<object> PostEvent;
        /// <summary>
        /// Очищает всех слушателей, отмеченных флагом 'isRemovable'/>.
        /// </summary>
        internal readonly Action CleanRemovableListenerEvent;

        private readonly Dictionary<Type, List<IEventListener>> _listeners;

        private bool _isDebuggable;

        private EventBus()
        {
            RegisterListenerEvent = RegisterListener;
            UnregisterListenerEvent = UnregisterListener;
            PostEvent = Post;
            CleanRemovableListenerEvent = CleanRemovableListener;

            _listeners = new Dictionary<Type, List<IEventListener>>();

            _isDebuggable = false;

            SceneManager.sceneUnloaded += delegate { CleanRemovableListener(); };
        }

        private void RegisterListener(Type messageType, IEventListener listener)
        {
            if (!_listeners.TryGetValue(messageType, out List<IEventListener> currentListeners))
            {
                currentListeners = new List<IEventListener>();

                _listeners.Add(messageType, currentListeners);
            }

            if (!currentListeners.Contains(listener))
                currentListeners.Add(listener);
        }

        private void UnregisterListener(Type messageType, int listenerHash)
        {
            if (_listeners.TryGetValue(messageType, out List<IEventListener> currentList))
                for (int i = 0; i < currentList.Count; ++i)
                    if (currentList[i].CheckListenerByHash(listenerHash))
                        currentList.Remove(currentList[i--]);
        }

        private void Post(object message)
        {
            if (_isDebuggable)
                Debug.Log($"Post Event: {message}");

            if (_listeners.TryGetValue(message.GetType(), out List<IEventListener> currentListeners))
                for (int i = 0; i < currentListeners.Count; ++i)
                    currentListeners[i].PostEvent(message);
        }

        private void CleanRemovableListener()
        {
            foreach (KeyValuePair<Type, List<IEventListener>> listener in _listeners)
            {
                List<IEventListener> value = listener.Value;

                for (int i = 0; i < value.Count; ++i)
                    if (value[i].IsRemovable())
                        value.Remove(value[i--]);
            }
        }
    }
}
