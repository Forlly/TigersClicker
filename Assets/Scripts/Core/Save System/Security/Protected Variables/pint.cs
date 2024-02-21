using System;
using UnityEngine;

namespace Project.Security
{
    [Serializable]
    public struct pint
    {
        [SerializeField]
        private int _encryptedValue;

        [SerializeField]
        private int _key;

        public pint(int value)
        {
#if UNITY_EDITOR
            _key = new System.Random().Next();
#else
            _key = Environment.TickCount + 1026;
#endif
            _encryptedValue = value + _key;
        }

        public override bool Equals(object obj)
        {
            return ((pint)obj).GetValue() == GetValue();
        }

        public override string ToString()
        {
            return GetValue().ToString();
        }

        public override int GetHashCode()
        {
            return GetValue().GetHashCode();
        }

        private int GetValue()
        {
            return _encryptedValue - _key;
        }

        public static implicit operator int(pint v) => v.GetValue();
        public static implicit operator double(pint v) => v.GetValue();
        public static implicit operator float(pint v) => v.GetValue();
        public static implicit operator pint(int v) => new pint(v);

        public static bool operator ==(pint a, pint b) => a.GetValue() == b.GetValue();
        public static bool operator !=(pint a, pint b) => a.GetValue() != b.GetValue();
        public static bool operator >(pint a, pint b) => a.GetValue() > b.GetValue();
        public static bool operator <(pint a, pint b) => a.GetValue() < b.GetValue();
        public static bool operator >=(pint a, pint b) => a.GetValue() >= b.GetValue();
        public static bool operator <=(pint a, pint b) => a.GetValue() <= b.GetValue();

        public static pint operator +(pint a, pint b) => new pint(a.GetValue() + b.GetValue());
        public static pint operator -(pint a, pint b) => new pint(a.GetValue() - b.GetValue());
        public static pint operator *(pint a, pint b) => new pint(a.GetValue() * b.GetValue());
        public static pint operator /(pint a, pint b) => new pint(a.GetValue() / b.GetValue());
        public static pint operator ^(pint a, pint b) => new pint(a.GetValue() ^ b.GetValue());
        public static pint operator >>(pint a, int b) => new pint(a.GetValue() >> b);
        public static pint operator <<(pint a, int b) => new pint(a.GetValue() << b);
    }
}
