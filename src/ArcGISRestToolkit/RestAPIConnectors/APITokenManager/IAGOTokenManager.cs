namespace ArcGISRestToolkit.RestAPIConnectors.APITokenManager
{
	public interface IAGOTokenManager
	{
		string CurrentAGOToken { get; }
		void Initialize();
	}
}