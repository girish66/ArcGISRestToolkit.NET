namespace ArcGISRestToolkit.RestAPIConnectors.APITokenManager
{
	public interface IAGOTokenConnector
	{
		bool IsTokenValid(string token);
		string GenerateToken();
	}
}