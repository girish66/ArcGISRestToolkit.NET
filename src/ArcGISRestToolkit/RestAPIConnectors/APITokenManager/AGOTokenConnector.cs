namespace ArcGISRestToolkit.RestAPIConnectors.APITokenManager
{
	using System;
	using ArcGISRestToolkit.Helper;
	using ArcGISRestToolkit.Models.AGOToken;
	using ArcGISRestToolkit.Models.RestAPI;
	using ArcGISRestToolkit.Resource;

	public class AGOTokenConnector : RestAPIBaseWebRequest, IAGOTokenConnector
	{
		#region Fields and Properties

		private const string GENERATE_TOKEN_REQUEST_PARAMS = "f=json&request=gettoken&username={0}&password={1}&referer={2}&expiration=525600";
		private const int TOKEN_INVALID_AGS_ERROR = 498;
		private const int TOKEN_REQUIRED_AGS_ERROR = 499;

		private readonly AGOTokenManagerParams _tokenManagerParams;

		#endregion

		#region Constructor

		public AGOTokenConnector(AGOTokenManagerParams tokenManagerParams)
		{
			this._tokenManagerParams = tokenManagerParams;
		}

		#endregion

		#region Public Methods

		public bool IsTokenValid(string token)
		{
			if (String.IsNullOrEmpty(token))
			{
				return false;
			}

			var serviceUrl = this._tokenManagerParams.AGOTestServiceUrl;
			var paramsService = String.Format("f=json&token={0}", token);
			var httpWebResponse = BaseWebRequest(serviceUrl, paramsService);

			try
			{
				if (httpWebResponse != null)
				{
					if (httpWebResponse.Contains("error"))
					{
						var error = Json.Deserialize<RestAPIResponseError>(httpWebResponse);
						return (error.Error.Code == TOKEN_INVALID_AGS_ERROR || (error.Error.Code == TOKEN_REQUIRED_AGS_ERROR));
					}

					return false;
				}

				throw new InvalidOperationException(ErrorMessages.msgAGOTokenManager_Error);
			}
			catch (Exception err)
			{
				throw new InvalidOperationException(ErrorMessages.msgAGOTokenManager_Error, err);
			}
		}

		public string GenerateToken()
		{
			var serviceParams = string.Format(GENERATE_TOKEN_REQUEST_PARAMS,
					this._tokenManagerParams.AGOUserName,
					this._tokenManagerParams.AGOUserPassword,
					this._tokenManagerParams.AGOForOrganizationsBaseUrl);

			var serviceUrl = this._tokenManagerParams.AGOGenerateTokenUrl;
			var httpWebResponse = BaseWebRequest(serviceUrl, serviceParams);

			try
			{
				if (httpWebResponse != null)
				{
					if (!httpWebResponse.Contains("error"))
					{
						var result = Json.Deserialize<AGOTokenManagerResponse>(httpWebResponse);
						return result.Token;
					}

					var error = Json.Deserialize<RestAPIResponseError>(httpWebResponse);
					var errorsDetails = String.Join(",", error.Error.Details.ToArray());
					var errorMessage = String.Format(ErrorMessages.msgAGOTokenManager_Result_Error, error.Error.Code, error.Error.Message, errorsDetails);

					throw new InvalidOperationException(errorMessage);
				}

				throw new InvalidOperationException(ErrorMessages.msgAGOTokenManager_Error);
			}
			catch (Exception err)
			{
				throw new InvalidOperationException(ErrorMessages.msgAGOTokenManager_Error, err);
			}
		}

		#endregion
	}
}