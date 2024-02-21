using System;
using UnityEngine.Scripting;

namespace Project.DependencyInjection.Core
{
    /// <summary>
    /// Базовый класс для регистрации сервиса.
    /// </summary>
    [RequireDerived]
    public abstract class DIModule
    {
        /// <summary>
        /// Вызывается при инициализации всех сервисов.
        /// </summary>
        protected abstract void Load();

        /// <summary>
        /// Привязывает интерфейс к сервису с кастомной функцией получения экземпляра сервиса.
        /// </summary>
        /// <param name="customGetInstanceFunc">Функция получения экземпляра сервиса.</param>
        protected void Register<TInterface, TService>(Func<object> customGetInstanceFunc) where TService : TInterface
        {
            DI.Register<TInterface, TService>(customGetInstanceFunc);
        }

        /// <summary>
        /// Привязывает интерфейс к сервису с функцией получения экземпляра сервиса по умолчанию.
        /// </summary>
        protected void Register<TInterface, TService>() where TService : TInterface, new()
        {
            DI.Register<TInterface, TService>();
        }
    }
}
