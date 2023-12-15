using Microsoft.Extensions.Configuration;
using CRM.Services.Abstract;
using System.Security.Cryptography;
using System.Text;

namespace CRM.Services.Concrete
{
	public class EncryptionManager : IEncryptionService
	{
		private readonly IConfiguration _conf;

        public EncryptionManager(IConfiguration conf)
        {
			_conf = conf;
        }

        public string GenerateRandomIV(int size)
		{
			byte[] iv = new byte[size];
			using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(iv);
			}
			return Encoding.ASCII.GetString(iv);
		}

		public string AESEncrypt(string plainText)
		{
			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			byte[] key = Encoding.UTF8.GetBytes(_conf["Aes:Key"]);
			byte[] iv = Encoding.UTF8.GetBytes(_conf["Aes:IV"]);

			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = key;
				aesAlg.IV = iv;

				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
						cryptoStream.FlushFinalBlock();
						return Convert.ToBase64String(memoryStream.ToArray());
					}
				}
			}
		}

        public string AESDecrypt(string cipherText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText); // Base64 çözümlemesi yapılıyor
            byte[] key = Encoding.UTF8.GetBytes(_conf["Aes:Key"]);
            byte[] iv = Encoding.UTF8.GetBytes(_conf["Aes:IV"]);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public string Base64Encode(string plainText)
		{
			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			return Convert.ToBase64String(plainTextBytes);
		}

		public string Base64Decode(string plainText)
		{
			var base64EncodedBytes = Convert.FromBase64String(plainText);
			return Encoding.UTF8.GetString(base64EncodedBytes);
		}

		public string MD5Encrypt(string plainText)
		{
			using (MD5 md5 = MD5.Create())
			{
				byte[] inputBytes = Encoding.ASCII.GetBytes(plainText);
				byte[] hashBytes = md5.ComputeHash(inputBytes);

				// Convert the byte array to hexadecimal string
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < hashBytes.Length; i++)
				{
					sb.Append(hashBytes[i].ToString("X2"));
				}
				return sb.ToString();
			}
		}
	}
}
