using System.Text;

namespace Project.Security
{
    public class FastEncryption
    {
        protected readonly byte[] MainKeyBytes;
        protected readonly byte[] AdditionalKeyBytes;

        public FastEncryption(pstring key)
        {
            MainKeyBytes = GetBytes(key);
            AdditionalKeyBytes = ProcessWithMainKey(GetBytes(key));
        }

        public string EncryptString(string data)
        {
            byte[] bytes = GetBytes(data);

            bytes = EncryptBytes(bytes);

            return GetString(bytes);
        }

        public string DecryptString(string data)
        {
            byte[] bytes = GetBytes(data);

            bytes = DecryptBytes(bytes);

            return GetString(bytes);
        }

        public byte[] EncryptBytes(byte[] data)
        {
            byte[] bytes = data;

            bytes = ProcessWithMainKey(bytes);
            bytes = ProcessWithAdditionalKey(bytes);

            return bytes;
        }

        public byte[] DecryptBytes(byte[] data)
        {
            byte[] bytes = data;

            bytes = ProcessWithMainKey(bytes);
            bytes = ProcessWithAdditionalKey(bytes);

            return bytes;
        }

        protected byte[] ProcessWithMainKey(byte[] data)
        {
            byte[] bytes = data;
            int j = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                j++;

                if (j == MainKeyBytes.Length)
                    j = 0;

                bytes[i] ^= MainKeyBytes[j];
            }

            return bytes;
        }

        protected byte[] ProcessWithAdditionalKey(byte[] data)
        {
            byte[] bytes = data;
            int j = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                j++;

                if (j == AdditionalKeyBytes.Length)
                    j = 0;

                bytes[i] ^= AdditionalKeyBytes[j];
            }

            return bytes;
        }

        protected byte[] GetBytes(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        protected string GetString(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
    }
}
