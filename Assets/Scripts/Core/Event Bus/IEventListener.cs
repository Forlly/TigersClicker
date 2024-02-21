namespace Project.EventBusSystem
{
    /// <summary>
    /// Слушатель события.
    /// </summary>
    public interface IEventListener
    {
        /// <summary>
        /// Отправляет событие.
        /// </summary>
        /// <param name="eventObject">Экземпляр события.</param>
        internal void PostEvent(object eventObject);
        /// <summary>
        /// Сравнивает проверяемый хеш с хешем слушателя.
        /// </summary>
        /// <returns>true, если проверяемый хеш совпадает с хешем слушателя.</returns>
        internal bool CheckListenerByHash(int hash);
        /// <summary>
        /// Может ли слушатель отписаться от события досрочно.
        /// </summary>
        bool IsRemovable();
    }
}
