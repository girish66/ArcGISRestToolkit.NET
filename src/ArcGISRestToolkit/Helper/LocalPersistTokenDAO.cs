namespace ArcGISRestToolkit.Helper
{
	using System.IO;

	public class LocalPersistTokenDAO : ILocalPersistTokenDAO
	{
		private const string CRYPTO_KEY = "IpsumLorem";

		public void SaveToken(string tokenFilePath, string tokenValue)
		{
			if (File.Exists(tokenFilePath))
			{
				File.Delete(tokenFilePath);
			}

			var sr = new StreamWriter(tokenFilePath);
			try
			{
				sr.WriteLine(Rijndael.Encrypt(tokenValue, CRYPTO_KEY));
			}
			finally
			{
				sr.Close();
				sr.Dispose();
			}
		}

		public string LoadToken(string tokenFilePath)
		{
			try
			{
				var streamPasswordFile = new StreamReader(tokenFilePath);

				try
				{
					var criptoToken = streamPasswordFile.ReadLine();
					var token = Rijndael.Decrypt(criptoToken, CRYPTO_KEY);
					return token;
				}
				finally
				{
					streamPasswordFile.Close();
					streamPasswordFile.Dispose();
				}
			}
			catch
			{
				return null;
			}
		}

	}
}
