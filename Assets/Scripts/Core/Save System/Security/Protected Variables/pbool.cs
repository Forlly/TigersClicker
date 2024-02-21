using System;
using UnityEngine;

namespace Project.Security
{
    [Serializable]
    public struct pbool
    {
        [SerializeField]
        private bool _encryptedValue;

        public pbool(bool value)
        {
            _encryptedValue = !value;
        }

        public override bool Equals(object obj)
        {
            return ((pbool)obj).GetValue() == GetValue();
        }

        public override string ToString()
        {
            return GetValue().ToString();
        }

        public override int GetHashCode()
        {
            return GetValue().GetHashCode();
        }

        private bool GetValue()
        {
            return !_encryptedValue;
        }

        public static implicit operator bool(pbool v) => v.GetValue();
        public static implicit operator pbool(bool v) => new pbool(v);

        public static bool operator ==(pbool a, pbool b) => a.GetValue() == b.GetValue();
        public static bool operator !=(pbool a, pbool b) => a.GetValue() != b.GetValue();

        public static bool operator ==(bool a, pbool b) => a == b.GetValue();
        public static bool operator !=(bool a, pbool b) => a != b.GetValue();

        public static bool operator ==(pbool a, bool b) => a.GetValue() == b;
        public static bool operator !=(pbool a, bool b) => a.GetValue() != b;
    }
}
