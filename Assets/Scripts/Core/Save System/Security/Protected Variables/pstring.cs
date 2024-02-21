using System;
using System.Text;
using UnityEngine;

namespace Project.Security
{
    [Serializable]
    public sealed class pstring
    {
        [SerializeField]
        private string _encryptedValue;

        [SerializeField]
        private byte _key;

        public pstring(string value)
        {
#if UNITY_EDITOR
            _key = (byte)new System.Random().Next(1, 120);
#else
            _key = (byte)((Environment.TickCount + 709) % 120);
#endif
            _encryptedValue = ProcessString(value);
        }

        public override bool Equals(object obj)
        {
            return ((pstring)obj).GetValue() == GetValue();
        }

        public override string ToString()
        {
            return GetValue().ToString();
        }

        public override int GetHashCode()
        {
            return GetValue().GetHashCode();
        }

        private string GetValue()
        {
            return ProcessString(_encryptedValue);
        }

        private string ProcessString(string value)
        {
            if (value == null)
                return null;

            byte[] bytes = GetBytes(value);

            for (int i = 0; i < bytes.Length; i++)
                bytes[i] ^= _key;

            return GetString(bytes);
        }

        private byte[] GetBytes(string value)
        {
            return Encoding.Unicode.GetBytes(value);
        }

        private string GetString(byte[] value)
        {
            return Encoding.Unicode.GetString(value);
        }

        public static implicit operator string(pstring v) => v.GetValue();
        public static implicit operator pstring(string v) => new pstring(v);

        public static bool operator ==(pstring a, pstring b) => a.GetValue() == b.GetValue();
        public static bool operator !=(pstring a, pstring b) => a.GetValue() != b.GetValue();

        public static pstring operator +(pstring a, pstring b) => new pstring(a.GetValue() + b.GetValue());
        public static pstring operator *(pstring a, int b)
        {
            string multiplier = a;
            string result = string.Empty;

            for (int i = 0; i < b; i++)
                result += multiplier;

            return new pstring(result);
        }
    }
}
