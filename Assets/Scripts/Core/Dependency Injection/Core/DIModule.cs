using System;
using UnityEngine.Scripting;

namespace Project.DependencyInjection.Core
{
    /// <summary>
    /// ������� ����� ��� ����������� �������.
    /// </summary>
    [RequireDerived]
    public abstract class DIModule
    {
        /// <summary>
        /// ���������� ��� ������������� ���� ��������.
        /// </summary>
        protected abstract void Load();

        /// <summary>
        /// ����������� ��������� � ������� � ��������� �������� ��������� ���������� �������.
        /// </summary>
        /// <param name="customGetInstanceFunc">������� ��������� ���������� �������.</param>
        protected void Register<TInterface, TService>(Func<object> customGetInstanceFunc) where TService : TInterface
        {
            DI.Register<TInterface, TService>(customGetInstanceFunc);
        }

        /// <summary>
        /// ����������� ��������� � ������� � �������� ��������� ���������� ������� �� ���������.
        /// </summary>
        protected void Register<TInterface, TService>() where TService : TInterface, new()
        {
            DI.Register<TInterface, TService>();
        }
    }
}
