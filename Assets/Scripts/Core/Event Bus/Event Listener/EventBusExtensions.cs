using System;

namespace Project
{
    public static partial class EventBusExtensions
    {
        /// <summary>
        /// Подписывает слушателя на событие.
        /// </summary>
        /// <typeparam name="T">Тип события.</typeparam>
        /// <param name="listenerAction">Метод, вызываемый при событии.</param>
        /// <param name="isRemovable">Может ли слушатель отписаться от события досрочно.</param>
        /// <returns>Экземпляр слушателя события.</returns>
        public static EventListener<T> AddListener<T>(this EventBus eventBus, Action<T> listenerAction, bool isRemovable = true) where T : struct
        {
            EventListener<T> listener = new EventListener<T>(listenerAction, isRemovable);

            eventBus.RegisterListenerEvent(typeof(T), listener);

            return listener;
        }

        /// <summary>
        /// Отписывает слушателя от события.
        /// </summary>
        /// <typeparam name="T">Тип события.</typeparam>
        /// <param name="listener">Экземпляр слушателя события.</param>
        public static void RemoveListener<T>(this EventBus eventBus, EventListener<T> listener) where T : struct
        {
            eventBus.UnregisterListenerEvent(typeof(T), listener.Hash);
        }

        /// <summary>
        /// Отправляет событие.
        /// </summary>
        /// <typeparam name="T">Тип события.</typeparam>
        /// <param name="message">Экземпляр события.</param>
        public static void Send<T>(this EventBus eventBus, T message) where T : struct
        {
            eventBus.PostEvent(message);
        }

        /// <summary>
        /// Очищает всех слушателей, отмеченных флагом 'isRemovable'.
        /// </summary>
        public static void CleanRemovableListeners(this EventBus eventBus)
        {
            eventBus.CleanRemovableListenerEvent();
        }
    }
}
