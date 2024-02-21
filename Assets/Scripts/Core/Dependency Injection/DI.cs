using Project.DependencyInjection.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.DependencyInjection
{
    /// <summary>
    /// Работа с DI.
    /// </summary>
    public static class DI
    {
        private static readonly Dictionary<Type, Func<object>> _services = new Dictionary<Type, Func<object>>();

        static DI()
        {
            DIRegistrator.Init();
        }

        /// <summary>
        /// Привязывает интерфейс к сервису с кастомной функцией получения экземпляра сервиса.
        /// </summary>
        /// <param name="getInstanceFunc">Функция получения экземпляра сервиса.</param>
        internal static void Register<TInterface, TService>(Func<object> getInstanceFunc) where TService : TInterface
        {
            Type interfaceType = typeof(TInterface);

            _services.Add(interfaceType, getInstanceFunc);
        }

        /// <summary>
        /// Привязывает интерфейс к сервису с функцией получения экземпляра сервиса по умолчанию.
        /// </summary>
        internal static void Register<TInterface, TService>() where TService : TInterface, new()
        {
            Type serviceType = typeof(TService);
            Func<object> getInstanceFunc = new Func<object>(() => Activator.CreateInstance(serviceType));

            Register<TInterface, TService>(getInstanceFunc);
        }

        /// <summary>
        /// Возвращает экземпляр сервиса, привязанного к интерфейсу.
        /// </summary>
        public static TInterface Get<TInterface>() where TInterface : IDIModule
        {
            Type interfaceType = typeof(TInterface);

#if UNITY_EDITOR
            if (!interfaceType.IsInterface)
                throw new Exception($"'{interfaceType.FullName}' is not an interface.");
#endif

            Func<object> serviceFunc = GetServiceFunc(interfaceType);

            return (TInterface)serviceFunc();
        }


        /// <summary>
        /// Возвращает экземпляр сервиса, привязанного к интерфейсу.
        /// </summary>
        public static async Task<TInterface> GetAsync<TInterface>() where TInterface : IAsyncDIModule
        {
            Type interfaceType = typeof(TInterface);

#if UNITY_EDITOR
            if (!interfaceType.IsInterface)
                throw new Exception($"'{interfaceType.FullName}' is not an interface.");
#endif

            Func<object> serviceFunc = GetServiceFunc(interfaceType);

            return await (Task<TInterface>)serviceFunc();
        }

        private static Func<object> GetServiceFunc(Type interfaceType)
        {
            _services.TryGetValue(interfaceType, out Func<object> serviceFunc);

            if (serviceFunc == null)
                throw new NullReferenceException($"'{interfaceType.FullName}' was not registered in DI.");

            return serviceFunc;
        }
    }
}
