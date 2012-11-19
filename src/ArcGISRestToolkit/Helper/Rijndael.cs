namespace ArcGISRestToolkit.Helper
{
	using System;
	using System.Text;
	using System.Security.Cryptography;
	using System.IO;

	public class Rijndael
	{
		public static string Encrypt(string text, string key)
		{
			var rijndael = new RijndaelManaged();
			var plainText = Encoding.Unicode.GetBytes(text);
			var salt = Encoding.ASCII.GetBytes(key.Length.ToString());
			var secretKey = new PasswordDeriveBytes(key, salt);

			var encryptor = rijndael.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
			var memoryStream = new MemoryStream();
			var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
			cryptoStream.Write(plainText, 0, plainText.Length);
			cryptoStream.FlushFinalBlock();
			var cipherBytes = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();

			return Convert.ToBase64String(cipherBytes);
		}

		public static string Decrypt(string text, string key)
		{
			var rijndael = new RijndaelManaged();
			var encryptedText = Convert.FromBase64String(text);
			var salt = Encoding.ASCII.GetBytes(key.Length.ToString());
			var secretKey = new PasswordDeriveBytes(key, salt);

			var decryptor = rijndael.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
			var memoryStream = new MemoryStream(encryptedText);
			var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
			byte[] plainText = new byte[encryptedText.Length];
			var decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
			memoryStream.Close();
			cryptoStream.Close();

			return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
		}
	}
}
