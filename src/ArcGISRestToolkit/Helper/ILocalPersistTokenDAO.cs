namespace ArcGISRestToolkit.Helper
{
	public interface ILocalPersistTokenDAO
	{
		void SaveToken(string tokenFilePath, string tokenValue);
		string LoadToken(string tokenFilePath);
	}
}