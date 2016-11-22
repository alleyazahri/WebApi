using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace JiraTasks.Data
{
    internal class LoginSettingsDocument
    {
        internal string Username { get; set; }
        internal string Password { get; set; }
        internal CheckState SavePassword { get; set; }
        private static string IV = "wquosit53865q0vz";
        private static string Key = "n7n4v359N*g567M956M`C2704[9M[Q'0";

        public void ClearUser()
        {
            Username = null;
            Password = null;
            SavePassword = CheckState.Unchecked;
        }

        public bool Load(string path, string filename)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                File.Create($@"{path}/{filename}").Dispose();
                return false;
            }
            var file = File.ReadAllText($@"{path}/{filename}");
            var data = file.Split('|');
            try
            {
                Username = Decrypt(data[0]);
                Password = Decrypt(data[1]);
                SavePassword = (CheckState)int.Parse(data[2]);
                if (Username == " ")
                    Username = null;
                if (Password == " ")
                    Password = null;
                return true;
            }
            catch (Exception)
            {
                File.Create($@"{path}/{filename}").Dispose();
                return false;
            }
        }

        public void Save(string path, string filename)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                File.Create($@"{path}/{filename}").Dispose();
            }
            if (!File.Exists($@"{path}/{filename}"))
            {
                File.Create($@"{path}/{filename}").Dispose();
            }

            using (var sw = new StreamWriter($@"{path}/{filename}", false))
            {
                sw.WriteLineAsync(SavePassword == CheckState.Checked
                    ? $"{Encrypt(Username)}|{Encrypt(Password)}|{(int)SavePassword}"
                    : $" | |{(int)SavePassword}");
            }
        }

        private string Decrypt(string value)
        {
            byte[] valueBytes = Convert.FromBase64String(value);
            var encdec = new AesCryptoServiceProvider
            {
                BlockSize = 128,
                KeySize = 256,
                Key = Encoding.ASCII.GetBytes(Key),
                IV = Encoding.ASCII.GetBytes(IV),
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC
            };

            var icrypt = encdec.CreateDecryptor(encdec.Key, encdec.IV);

            byte[] dec = icrypt.TransformFinalBlock(valueBytes, 0, valueBytes.Length);
            icrypt.Dispose();

            return Encoding.ASCII.GetString(dec);
        }

        private string Encrypt(string value)
        {
            byte[] valueBytes = Encoding.ASCII.GetBytes(value);
            var encdec = new AesCryptoServiceProvider
            {
                BlockSize = 128,
                KeySize = 256,
                Key = Encoding.ASCII.GetBytes(Key),
                IV = Encoding.ASCII.GetBytes(IV),
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC
            };

            var icrypt = encdec.CreateEncryptor(encdec.Key, encdec.IV);

            byte[] enc = icrypt.TransformFinalBlock(valueBytes, 0, valueBytes.Length);
            icrypt.Dispose();

            return Convert.ToBase64String(enc);
        }
    }
}