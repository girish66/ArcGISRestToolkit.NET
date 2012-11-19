namespace ArcGISRestToolkit.RestAPIConnectors
{
	using System.Collections.Generic;
	using ArcGISRestToolkit.Models.RestAPI;

	public interface IRestAPIFeatureServiceConnector<TF, TG, TA>
		where TF : IRestAPIFeature<TG, TA>
		where TG : IRestAPIGeometry
		where TA : IRestAPIAttribute
	{
		int QueryFeaturesCountOnly(string whereClause);
		RestAPIQueryIDsOnlyResponse QueryFeaturesIDsOnly(string whereClause);

		List<TF> QueryFeaturesByWhereClause(string whereClause, bool returnGeometries = true);
		List<TF> QueryFeaturesByIDs(List<long> objectIDList, bool returnGeometries = true);
		List<TF> QueryFeaturesByWhereClauseReturnSelectedFields(string whereClause, List<string> outFieldsList, bool returnGeometries = true);

		bool AddFeatures(List<TF> featuresToAdd, out List<RestAPIOperationResponseBase> errorList);
		bool UpdateFeatures(List<TF> featuresToUpdate, out List<RestAPIOperationResponseBase> errorList);
		bool DeleteFeatures(List<long> objectIDList, out List<RestAPIOperationResponseBase> errorList);
	}
}
