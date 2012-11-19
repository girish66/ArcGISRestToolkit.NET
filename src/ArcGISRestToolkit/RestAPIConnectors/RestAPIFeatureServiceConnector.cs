namespace ArcGISRestToolkit.RestAPIConnectors
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using ArcGISRestToolkit.Helper;
	using ArcGISRestToolkit.Models.RestAPI;
	using ArcGISRestToolkit.Resource;
	using ArcGISRestToolkit.RestAPIConnectors.APITokenManager;

	/// <summary>
	/// Responsible for providing basic CRUD functionalities that consumes Esri REST services (ArcGIS Server or ArcGIS Online).
	///		Provides functionalities like: Search by Where Clause, Retrieve Ammount of Features, Search by IDs, Add, Update and Delete Features.
	/// </summary>
	/// <typeparam name="TF">Type of the Feature this connector will handle. Must implements IRestAPIFeature.</typeparam>
	/// <typeparam name="TG">Type of the Geometry handled by the Feature. Must implements IRestAPIGeometry.</typeparam>
	/// <typeparam name="TA">Type of the Attributes handled by the Feature. Must implements IRestAPIAttribute.</typeparam>
	public class RestAPIFeatureServiceConnector<TF, TG, TA> : RestAPIBaseWebRequest, IRestAPIFeatureServiceConnector<TF, TG, TA>
		where TF : IRestAPIFeature<TG, TA>
		where TG : IRestAPIGeometry
		where TA : IRestAPIAttribute
	{
		#region Fields and Properties

		private const string CU_FEATURES_REQUEST_PARAMS = "f=pjson&token={0}&features={1}";
		private const string CU_FEATURES_REQUEST_PARAMS_WITHOUT_TOKEN = "f=pjson&features={0}";
		private const string D_FEATURES_REQUEST_PARAMS = "f=pjson&objectIds={0}&where={1}&token={2}";
		private const string D_FEATURES_REQUEST_PARAMS_WITHOUT_TOKEN = "f=pjson&objectIds={0}&where={1}";
		private const string ADD_FEATURES_URL_SECTION = "/addFeatures";
		private const string UPDATE_FEATURES_URL_SECTION = "/updateFeatures";
		private const string DELETE_FEATURES_URL_SECTION = "/deleteFeatures";
		private const string QUERY_FEATURES_URL_SECTION = "/query";
		private const string QUERY_FEATURES_REQUEST_PARAMS = "objectIds={0}&where={1}&time=&geometry={2}&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&relationParam=&outFields={3}&returnGeometry={4}&outSR=&returnCountOnly={5}&returnIdsOnly={6}&f=pjson&token={7}";
		private const string QUERY_FEATURES_REQUEST_PARAMS_WITHOUT_TOKEN = "objectIds={0}&where={1}&time=&geometry={2}&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&relationParam=&outFields={3}&returnGeometry={4}&outSR=&returnCountOnly={5}&returnIdsOnly={6}&f=pjson";

		private readonly IAGOTokenManager _agoTokenManager;
		private readonly string _featureServiceBaseUrl;

		#endregion

		#region Constructor

		public RestAPIFeatureServiceConnector(string featureServiceBaseUrl)
			: this(null, featureServiceBaseUrl)
		{ }

		public RestAPIFeatureServiceConnector(IAGOTokenManager agoTokenManager, string featureServiceBaseUrl)
		{
			if (string.IsNullOrEmpty(featureServiceBaseUrl)) { throw new ArgumentNullException("featureServiceBaseUrl"); }

			this._agoTokenManager = agoTokenManager;
			this._featureServiceBaseUrl = featureServiceBaseUrl;
		}

		#endregion

		#region Public Methods

		public int QueryFeaturesCountOnly(string whereClause)
		{
			if (whereClause == null) { throw new ArgumentNullException("whereClause"); }
			if (whereClause == string.Empty) { throw new ArgumentException(ErrorMessages.msgFeatureServiceConnector_Error_BlankWhereClause, "whereClause"); }

			var httpWebResponse = QueryFeaturesBase(string.Empty, whereClause, string.Empty, false, true, false);
			if (httpWebResponse != null)
			{
				var result = Json.Deserialize<RestAPIQueryCountOnlyResponse>(httpWebResponse);
				return result.Count;
			}

			return -1;
		}

		public RestAPIQueryIDsOnlyResponse QueryFeaturesIDsOnly(string whereClause)
		{
			if (whereClause == null) { throw new ArgumentNullException("whereClause"); }
			if (whereClause == string.Empty) { throw new ArgumentException(ErrorMessages.msgFeatureServiceConnector_Error_BlankWhereClause, "whereClause"); }

			var httpWebResponse = QueryFeaturesBase(string.Empty, whereClause, string.Empty, false, true, false);
			if (httpWebResponse != null)
			{
				var result = Json.Deserialize<RestAPIQueryIDsOnlyResponse>(httpWebResponse);
				return result;
			}

			return null;
		}

		public List<TF> QueryFeaturesByWhereClause(string whereClause, bool returnGeometries = true)
		{
			if (whereClause == null) { throw new ArgumentNullException("whereClause"); }
			if (whereClause == string.Empty) { throw new ArgumentException(ErrorMessages.msgFeatureServiceConnector_Error_BlankWhereClause, "whereClause"); }

			return this.QueryFeaturesCommonMethod(string.Empty, whereClause, "*", returnGeometries);
		}

		public List<TF> QueryFeaturesByIDs(List<long> objectIDList, bool returnGeometries = true)
		{
			if (objectIDList == null) { throw new ArgumentNullException("objectIDList"); }

			var objectIDs = string.Join(",", objectIDList.ToArray());
			return this.QueryFeaturesCommonMethod(objectIDs, string.Empty, "*", returnGeometries);
		}

		public List<TF> QueryFeaturesByWhereClauseReturnSelectedFields(string whereClause, List<string> outFieldsList, bool returnGeometries = true)
		{
			if (whereClause == null) { throw new ArgumentNullException("whereClause"); }
			if (whereClause == string.Empty) { throw new ArgumentException(ErrorMessages.msgFeatureServiceConnector_Error_BlankWhereClause, "whereClause"); }
			if (outFieldsList == null) { throw new ArgumentNullException("outFieldsList"); }
			if (outFieldsList.Any(x => x == null)) { throw new ArgumentException(ErrorMessages.msgFeatureServiceConnector_Error_NullItemInOutFieldsList, "outFieldsList"); }

			var outFields = string.Join(",", outFieldsList.ToArray());
			return this.QueryFeaturesCommonMethod(string.Empty, whereClause, outFields, returnGeometries);
		}

		public bool AddFeatures(List<TF> featuresToAdd, out List<RestAPIOperationResponseBase> errorList)
		{
			if (featuresToAdd == null) { throw new ArgumentNullException("featuresToAdd"); }
			if (featuresToAdd.Any(x => Equals(x, default(TF)))) { throw new ArgumentException(ErrorMessages.msgFeatureServiceConnector_Error_NullFeatureInTheList, "featuresToAdd"); }

			errorList = new List<RestAPIOperationResponseBase>();
			var addFeaturesUrl = string.Format("{0}{1}", _featureServiceBaseUrl, ADD_FEATURES_URL_SECTION);
			var serviceParams = CreateCURequestParams(featuresToAdd);

			var httpWebResponse = BaseWebRequest(addFeaturesUrl, serviceParams);
			if (httpWebResponse != null)
			{
				var addNewFeaturesResult = Json.Deserialize<RestAPIAddOperationResponse>(httpWebResponse);
				errorList.AddRange(addNewFeaturesResult.Responses);
			}

			return errorList.All(x => x.Success);
		}

		public bool UpdateFeatures(List<TF> featuresToUpdate, out List<RestAPIOperationResponseBase> errorList)
		{
			if (featuresToUpdate == null) { throw new ArgumentNullException("featuresToUpdate"); }
			if (featuresToUpdate.Any(x => Equals(x, default(TF)))) { throw new ArgumentException(ErrorMessages.msgFeatureServiceConnector_Error_NullFeatureInTheList, "featuresToUpdate"); }

			errorList = new List<RestAPIOperationResponseBase>();
			var updateFeaturesUrl = string.Format("{0}{1}", _featureServiceBaseUrl, UPDATE_FEATURES_URL_SECTION);
			var serviceParams = CreateCURequestParams(featuresToUpdate);

			var httpWebResponse = BaseWebRequest(updateFeaturesUrl, serviceParams);
			if (httpWebResponse != null)
			{
				var updateFeaturesResult = Json.Deserialize<RestAPIUpdateOperationResponse>(httpWebResponse);
				errorList.AddRange(updateFeaturesResult.Responses);
			}

			return errorList.All(x => x.Success);
		}

		public bool DeleteFeatures(List<long> objectIDList, out List<RestAPIOperationResponseBase> errorList)
		{
			if (objectIDList == null) { throw new ArgumentNullException("objectIDList"); }

			errorList = new List<RestAPIOperationResponseBase>();
			var deleteFeaturesUrl = string.Format("{0}{1}", _featureServiceBaseUrl, DELETE_FEATURES_URL_SECTION);

			var objectIDs = string.Join(",", objectIDList.ToArray());
			var serviceParams = CreateDRequestParams(objectIDs, string.Empty);

			var httpWebResponse = BaseWebRequest(deleteFeaturesUrl, serviceParams);
			if (httpWebResponse != null)
			{
				var deleteFeaturesResult = Json.Deserialize<RestAPIDeleteOperationResponse>(httpWebResponse);
				errorList.AddRange(deleteFeaturesResult.Responses);
			}

			return errorList.All(x => x.Success);
		}

		#endregion

		#region Private Methods

		private List<TF> QueryFeaturesCommonMethod(string objectIDsList, string whereClause, string outFields, bool returnGeoemtries)
		{
			var response = this.QueryFeaturesBase(objectIDsList, whereClause, outFields, returnGeoemtries, false, false);

			if (response != null)
			{
				if (!response.Contains("error"))
				{
					var result = Json.Deserialize<RestAPIReadOperationResponse<TF, TG, TA>>(response);
					return result.Features;
				}

				var error = Json.Deserialize<RestAPIResponseError>(response);
				var errorsDetails = String.Join(",", error.Error.Details.ToArray());
				var errorMessage = String.Format(ErrorMessages.msgFeatureServiceConnector_Result_Error, error.Error.Code, error.Error.Message, errorsDetails);
				throw new InvalidOperationException(errorMessage);
			}

			throw new InvalidOperationException(ErrorMessages.msgFeatureServiceConnector_Error_Query);
		}

		private string QueryFeaturesBase(string objectIDs, string whereClause, string outFields, bool returnGeometries, bool returnCountOnly, bool returnIdsOnly)
		{
			var queryFeaturesCountUrl = string.Format("{0}{1}", _featureServiceBaseUrl, QUERY_FEATURES_URL_SECTION);
			var serviceParams = CreateQueryRequestParams(objectIDs, whereClause, outFields, returnGeometries, returnCountOnly, returnIdsOnly);

			var httpWebResponse = BaseWebRequest(queryFeaturesCountUrl, serviceParams);
			return httpWebResponse;
		}

		private string CreateCURequestParams(List<TF> featuresList)
		{
			var serializedFeatures = Json.Serialize(featuresList);

			var requestParams = this._agoTokenManager == null ?
				string.Format(CU_FEATURES_REQUEST_PARAMS_WITHOUT_TOKEN, serializedFeatures) :
				string.Format(CU_FEATURES_REQUEST_PARAMS, this._agoTokenManager.CurrentAGOToken, serializedFeatures);

			return requestParams;
		}

		private string CreateDRequestParams(string objectIDs, string whereClause)
		{
			var requestParams = this._agoTokenManager == null ?
				string.Format(D_FEATURES_REQUEST_PARAMS_WITHOUT_TOKEN, objectIDs, whereClause) :
				string.Format(D_FEATURES_REQUEST_PARAMS, objectIDs, whereClause, this._agoTokenManager.CurrentAGOToken);

			return requestParams;
		}

		private string CreateQueryRequestParams(string objectIDs, string whereClause, string outFields, bool returnGeometries, bool returnCountOnly, bool returnIdsOnly)
		{
			string requestParams;
			if (this._agoTokenManager == null)
			{
				requestParams = string.Format(QUERY_FEATURES_REQUEST_PARAMS_WITHOUT_TOKEN,
					objectIDs,
					whereClause,
					string.Empty,
					outFields,
					returnGeometries,
					returnCountOnly,
					returnIdsOnly);
			}
			else
			{
				requestParams = string.Format(QUERY_FEATURES_REQUEST_PARAMS,
					objectIDs,
					whereClause,
					string.Empty,
					outFields,
					returnGeometries,
					returnCountOnly,
					returnIdsOnly,
					_agoTokenManager.CurrentAGOToken);
			}

			return requestParams;
		}

		#endregion
	}
}
