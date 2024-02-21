using System;
using System.Linq;
using System.Reflection;

namespace Project.DependencyInjection.Core
{
    /// <summary>
    /// ����������� ��������.
    /// </summary>
    internal sealed class DIRegistrator
    {
        /// <summary>
        /// �������������� ��� �������.
        /// </summary>
        internal static void Init()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Type moduleType = typeof(DIModule);

            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes().Where(v => v.IsClass && !v.IsAbstract && v.IsSubclassOf(moduleType)).ToArray();

                foreach (Type type in types)
                {
                    object obj = Activator.CreateInstance(type);

                    type.GetMethod("Load", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(obj, null);
                }
            }
        }
    }
}
