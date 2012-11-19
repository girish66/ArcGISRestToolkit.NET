namespace ArcGISRestToolkit.RestAPIConnectors.APITokenManager
{
	using ArcGISRestToolkit.Helper;

	class AGOTokenManager : IAGOTokenManager
	{
		private const string TOKEN_FILE = "TokenFile.sys";

		private readonly IAGOTokenConnector _agoGenerateTokenDAO;
		private readonly ILocalPersistTokenDAO _localPersistTokenDAO;

		public string CurrentAGOToken { get; private set; }

		public AGOTokenManager(IAGOTokenConnector agoGenerateTokenDAO, ILocalPersistTokenDAO localPersistTokenDAO)
		{
			this._agoGenerateTokenDAO = agoGenerateTokenDAO;
			this._localPersistTokenDAO = localPersistTokenDAO;
		}

		public void Initialize()
		{
			var token = this._localPersistTokenDAO.LoadToken(TOKEN_FILE);

			if (!this._agoGenerateTokenDAO.IsTokenValid(token))
			{
				token = this._agoGenerateTokenDAO.GenerateToken();
				this._localPersistTokenDAO.SaveToken(TOKEN_FILE, token);
			}

			this.CurrentAGOToken = token;
		}
	}
}
